/// <summary>
/// Copyright (c) 2001-2012 Steve Purcell.
/// Copyright (c) 2002      Vidar Holen.
/// Copyright (c) 2002      Michal Ceresna.
/// Copyright (c) 2005      Ewan Mellor.
/// Copyright (c) 2010-2012 penSec.IT UG (haftungsbeschränkt).
/// 
/// (c) 2026 adaptations to port to C# and to the GrGen project coding style by Edgar Jakumeit
/// 
/// All rights reserved.
/// 
/// Redistribution and use in source and binary forms, with or without
/// modification, are permitted provided that the following conditions are met:
/// Redistributions of source code must retain the above copyright notice, this
/// list of conditions and the following disclaimer. Redistributions in binary
/// form must reproduce the above copyright notice, this list of conditions and
/// the following disclaimer in the documentation and/or other materials provided
/// with the distribution. Neither the name of the copyright holder nor the names
/// of its contributors may be used to endorse or promote products derived from
/// this software without specific prior written permission.
/// 
/// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
/// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
/// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
/// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR CONTRIBUTORS BE
/// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
/// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
/// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
/// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
/// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
/// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
/// POSSIBILITY OF SUCH DAMAGE.
/// </summary>

namespace com.sanityinc.jargs
{

	using System;
	using System.Collections.Generic;
	using System.Globalization;

	/// <summary>
	/// Largely GNU-compatible command-line options parser. Has short (-v) and
	/// long-form (--verbose) option support, and also allows options with
	/// associated values (-d 2, --debug 2, --debug=2). Option processing
	/// can be explicitly terminated by the argument '--'.
	/// 
	/// @author Steve Purcell
	/// @author penSec.IT UG (haftungsbeschränkt)
	/// 
	/// @version 2.0 </summary>
	/// <seealso cref="com.sanityinc.jargs.examples.OptionTest"/>
	public class CmdLineParser
	{
		/// <summary>
		/// Base class for exceptions that may be thrown when options are parsed
		/// </summary>
		public abstract class OptionException : Exception
		{
			internal OptionException(string msg) : base(msg)
			{
			}
		}

		/// <summary>
		/// Thrown when the parsed command-line contains an option that is not
		/// recognised. <code>getMessage()</code> returns
		/// an error string suitable for reporting the error to the user (in
		/// English).
		/// </summary>
		public class UnknownOptionException : OptionException
		{
			internal UnknownOptionException(string optionName)
				: this(optionName, "Unknown option '" + optionName + "'")
			{
			}

			internal UnknownOptionException(string optionName, string msg)
				: base(msg)
			{
				this.optionName = optionName;
			}

			/// <returns> the name of the option that was unknown (e.g. "-u") </returns>
			public virtual string OptionName
			{
				get
				{
					return this.optionName;
				}
			}

			internal readonly string optionName;
		}

		/// <summary>
		/// Thrown when the parsed commandline contains multiple concatenated
		/// short options, such as -abcd, where one is unknown.
		/// <code>getMessage()</code> returns an english human-readable error
		/// string.
		/// @author Vidar Holen
		/// </summary>
		public class UnknownSuboptionException : UnknownOptionException
		{
			internal char suboption;

			internal UnknownSuboptionException(string option, char suboption)
				: base(option, "Illegal option: '" + suboption + "' in '" + option + "'")
			{
				this.suboption = suboption;
			}

			public virtual char Suboption
			{
				get
				{
					return suboption;
				}
			}
		}

		/// <summary>
		/// Thrown when the parsed commandline contains multiple concatenated
		/// short options, such as -abcd, where one or more requires a value.
		/// <code>getMessage()</code> returns an english human-readable error
		/// string.
		/// @author Vidar Holen
		/// </summary>
		public class NotFlagException : UnknownOptionException
		{
			internal char notflag;

			internal NotFlagException(string option, char unflaggish)
				: base(option, "Illegal option: '" + option + "', '" +
					unflaggish + "' requires a value")
			{
				notflag = unflaggish;
			}

			/// <returns> the first character which wasn't a boolean (e.g 'c') </returns>
			public virtual char OptionChar
			{
				get
				{
					return notflag;
				}
			}
		}

		/// <summary>
		/// Thrown when an illegal or missing value is given by the user for
		/// an option that takes a value. <code>getMessage()</code> returns
		/// an error string suitable for reporting the error to the user (in
		/// English).
		/// 
		/// No generic class can ever extend <code>java.lang.Throwable</code>, so we
		/// have to return <code>Option&lt;?&gt;</code> instead of
		/// <code>Option&lt;T&gt;</code>.
		/// </summary>
		public class IllegalOptionValueException : OptionException
		{
			internal IllegalOptionValueException(string msg, OptionBase opt, string value)
				: base(msg)
			{
				this.option = opt;
				this.value = value;
			}

			public static IllegalOptionValueException CreateIllegalOptionValueException<T>(Option<T> opt, string value)
			{
				return new IllegalOptionValueException("Illegal value '" + value + "' for option " +
						(!string.ReferenceEquals(opt.ShortForm, null) ? "-" + opt.ShortForm + "/" : "") +
						"--" + opt.LongForm, opt, value);
			}

			/// <returns> the name of the option whose value was illegal (e.g. "-u") </returns>
			public virtual OptionBase Option
			{
				get
				{
					return this.option;
				}
			}

			/// <returns> the illegal value </returns>
			public virtual string Value
			{
				get
				{
					return this.value;
				}
			}

			internal readonly OptionBase option;
			internal readonly string value;
		}

		public abstract class OptionBase
		{
			public abstract bool WantsValue();
			public abstract string ShortForm {get;}
			public abstract string LongForm {get;}
			public abstract object GetValue(string arg, CultureInfo locale);
		}

		/// <summary>
		/// Representation of a command-line option
		/// </summary>
		/// <param name="T"> Type of data configured by this option </param>
		public abstract class Option<T> : OptionBase
		{
			protected internal Option(string longForm, bool wantsValue)
				: this(null, longForm, wantsValue)
			{
			}

			protected internal Option(char shortForm, string longForm, bool wantsValue)
				: this(new string(new char[]{shortForm}), longForm, wantsValue)
			{
			}

			internal Option(string shortForm, string longForm, bool wantsValue)
			{
				if(string.ReferenceEquals(longForm, null))
					throw new ArgumentException("Null longForm not allowed");
				this.shortForm = shortForm;
				this.longForm = longForm;
				this.wantsValue_ = wantsValue;
			}

			public override string ShortForm
			{
				get
				{
					return this.shortForm;
				}
			}

			public override string LongForm
			{
				get
				{
					return this.longForm;
				}
			}

			/// <summary>
			/// Tells whether or not this option wants a value
			/// </summary>
			public override bool WantsValue()
			{
				return this.wantsValue_;
			}

			public T GetValueExact(string arg, CultureInfo locale)
			{
				if(this.wantsValue_)
				{
					if(string.ReferenceEquals(arg, null))
						throw IllegalOptionValueException.CreateIllegalOptionValueException(this, "");
					return this.ParseValue(arg, locale);
				}
				else
					return this.DefaultValue;
			}

			public sealed override object GetValue(string arg, CultureInfo locale)
			{
				return this.GetValueExact(arg, locale);
			}

			/// <summary>
			/// Override to extract and convert an option value passed on the
			/// command-line
			/// </summary>
			protected internal virtual T ParseValue(string arg, CultureInfo locale)
			{
				return default(T);
			}

			/// <summary>
			/// Override to define default value returned by getValue if option does
			/// not want a value
			/// </summary>
			protected internal virtual T DefaultValue
			{
				get
				{
					return default(T);
				}
			}

			internal readonly string shortForm;
			internal readonly string longForm;
			internal readonly bool wantsValue_;
		}

		/// <summary>
		/// An option that expects a boolean value
		/// </summary>
		public class BooleanOption : Option<bool>
		{
			public BooleanOption(char shortForm, string longForm)
				: base(shortForm, longForm, false)
			{
			}

			public BooleanOption(string longForm)
				: base(longForm, false)
			{
			}

			protected internal override bool ParseValue(string arg, CultureInfo lcoale)
			{
				return true;
			}

			protected internal override bool DefaultValue
			{
				get
				{
					return true;
				}
			}
		}

		/// <summary>
		/// An option that expects an integer value
		/// </summary>
		public class IntegerOption : Option<int>
		{
			public IntegerOption(char shortForm, string longForm)
				: base(shortForm, longForm, true)
			{
			}

			public IntegerOption(string longForm)
				: base(longForm, true)
			{
			}

			protected internal override int ParseValue(string arg, CultureInfo locale)
			{
				try
				{
					return Convert.ToInt32(arg);
				}
				catch(System.FormatException)
				{
					throw IllegalOptionValueException.CreateIllegalOptionValueException(this, arg);
				}
			}
		}

		/// <summary>
		/// An option that expects a long integer value
		/// </summary>
		public class LongOption : Option<long>
		{
			public LongOption(char shortForm, string longForm)
				: base(shortForm, longForm, true)
			{
			}

			public LongOption(string longForm)
				: base(longForm, true)
			{
			}

			protected internal override long ParseValue(string arg, CultureInfo locale)
			{
				try
				{
					return Convert.ToInt64(arg);
				}
				catch(System.FormatException)
				{
					throw IllegalOptionValueException.CreateIllegalOptionValueException(this, arg);
				}
			}
		}

		/// <summary>
		/// An option that expects a floating-point value
		/// </summary>
		public class DoubleOption : Option<double>
		{
			public DoubleOption(char shortForm, string longForm)
				: base(shortForm, longForm, true)
			{
			}

			public DoubleOption(string longForm)
				: base(longForm, true)
			{
			}

			protected internal override double ParseValue(string arg, CultureInfo locale)
			{
				try
				{
					return double.Parse(arg, locale);
				}
				catch(FormatException) // ParseException
				{
					throw IllegalOptionValueException.CreateIllegalOptionValueException(this, arg);
				}
			}
		}

		/// <summary>
		/// An option that expects a string value
		/// </summary>
		public class StringOption : Option<string>
		{
			public StringOption(char shortForm, string longForm)
				: base(shortForm, longForm, true)
			{
			}

			public StringOption(string longForm)
				: base(longForm, true)
			{
			}

			protected internal override string ParseValue(string arg, CultureInfo locale)
			{
				return arg;
			}
		}

		/// <summary>
		/// Add the specified Option to the list of accepted options
		/// </summary>
		public Option<T> AddOption<T>(Option<T> opt)
		{
			if(!string.ReferenceEquals(opt.ShortForm, null))
				this.options["-" + opt.ShortForm] = opt;
			this.options["--" + opt.LongForm] = opt;
			return opt;
		}

		/// <summary>
		/// Convenience method for adding a string option. </summary>
		/// <returns> the new Option </returns>
		public Option<string> AddStringOption(char shortForm, string longForm)
		{
			return AddOption(new StringOption(shortForm, longForm));
		}

		/// <summary>
		/// Convenience method for adding a string option. </summary>
		/// <returns> the new Option </returns>
		public Option<string> AddStringOption(string longForm)
		{
			return AddOption(new StringOption(longForm));
		}

		/// <summary>
		/// Convenience method for adding an integer option. </summary>
		/// <returns> the new Option </returns>
		public Option<int> AddIntegerOption(char shortForm, string longForm)
		{
			return AddOption(new IntegerOption(shortForm, longForm));
		}

		/// <summary>
		/// Convenience method for adding an integer option. </summary>
		/// <returns> the new Option </returns>
		public Option<int> AddIntegerOption(string longForm)
		{
			return AddOption(new IntegerOption(longForm));
		}

		/// <summary>
		/// Convenience method for adding a long integer option. </summary>
		/// <returns> the new Option </returns>
		public Option<long> AddLongOption(char shortForm, string longForm)
		{
			return AddOption(new LongOption(shortForm, longForm));
		}

		/// <summary>
		/// Convenience method for adding a long integer option. </summary>
		/// <returns> the new Option </returns>
		public Option<long> AddLongOption(string longForm)
		{
			return AddOption(new LongOption(longForm));
		}

		/// <summary>
		/// Convenience method for adding a double option. </summary>
		/// <returns> the new Option </returns>
		public Option<double> AddDoubleOption(char shortForm, string longForm)
		{
			return AddOption(new DoubleOption(shortForm, longForm));
		}

		/// <summary>
		/// Convenience method for adding a double option. </summary>
		/// <returns> the new Option </returns>
		public Option<double> AddDoubleOption(string longForm)
		{
			return AddOption(new DoubleOption(longForm));
		}

		/// <summary>
		/// Convenience method for adding a boolean option. </summary>
		/// <returns> the new Option </returns>
		public Option<bool> AddBooleanOption(char shortForm, string longForm)
		{
			return AddOption(new BooleanOption(shortForm, longForm));
		}

		/// <summary>
		/// Convenience method for adding a boolean option. </summary>
		/// <returns> the new Option </returns>
		public Option<bool> AddBooleanOption(string longForm)
		{
			return AddOption(new BooleanOption(longForm));
		}

		/// <summary>
		/// Equivalent to {@link #getOptionValue(Option, Object) getOptionValue(o,
		/// null)}.
		/// </summary>
		public T GetOptionValue<T>(Option<T> o)
		{
			return GetOptionValue(o, default(T));
		}


		/// <returns> the parsed value of the given Option, or the given default 'def'
		/// if the option was not set </returns>
		public T GetOptionValue<T>(Option<T> o, T def)
		{
			IList<object> v = values[o.LongForm];

			if(v == null)
				return def;
			else if(v.Count == 0)
				return default(T);
			else
			{
				/* Cast should be safe because Option.parseValue has to return an
				 * instance of type T or null
				 */
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: @SuppressWarnings("unchecked") T result = (T)v.remove(0);
				T result = (T)v.RemoveAndReturn(0);
				return result;
			}
		}


		/// <returns> A Collection giving the parsed values of all the occurrences of
		/// the given Option, or an empty Collection if the option was not set. </returns>
		public ICollection<T> GetOptionValues<T>(Option<T> option)
		{
			ICollection<T> result = new List<T>();

			while(true)
			{
				T o = GetOptionValue(option, default(T));

				if(o == null)
					return result;
				else
					result.Add(o);
			}
		}


		/// <returns> the non-option arguments </returns>
		public string[] RemainingArgs
		{
			get
			{
				return this.remainingArgs;
			}
		}

		/// <summary>
		/// Extract the options and non-option arguments from the given
		/// list of command-line arguments. The default locale is used for
		/// parsing options whose values might be locale-specific.
		/// </summary>
		public void Parse(string[] argv)
		{
			Parse(argv, CultureInfo.CurrentCulture); // Locale.GetDefault()
		}

		/// <summary>
		/// Extract the options and non-option arguments from the given
		/// list of command-line arguments. The specified locale is used for
		/// parsing options whose values might be locale-specific.
		/// </summary>
		public void Parse(string[] argv, CultureInfo locale)
		{
			List<object> otherArgs = new List<object>();
			int position = 0;
			this.values = new Dictionary<string, IList<object>>(10);
			while(position < argv.Length)
			{
				string curArg = argv[position];
				if(curArg.StartsWith("-", StringComparison.Ordinal))
				{
					if(curArg.Equals("--"))
					{ // end of options
						position += 1;
						break;
					}
					string valueArg = null;
					if(curArg.StartsWith("--", StringComparison.Ordinal))
					{ // handle --arg=value
						int equalsPos = curArg.IndexOf("=", StringComparison.Ordinal);
						if(equalsPos != -1)
						{
							valueArg = curArg.Substring(equalsPos + 1);
							curArg = curArg.Substring(0, equalsPos);
						}
					}
					else if(curArg.Length > 2)
					{ // handle -abcd
						for(int i = 1; i < curArg.Length; i++)
						{
							OptionBase shortOpt = this.options["-" + curArg[i]];
							if(shortOpt == null)
								throw new UnknownSuboptionException(curArg,curArg[i]);
							if(shortOpt.WantsValue())
								throw new NotFlagException(curArg,curArg[i]);
							AddValue(shortOpt, null, locale);

						}
						position++;
						continue;
					}

					OptionBase opt = this.options[curArg];
					if(opt == null)
						throw new UnknownOptionException(curArg);

					if(opt.WantsValue())
					{
						if(string.ReferenceEquals(valueArg, null))
						{
							position += 1;
							if(position < argv.Length)
								valueArg = argv[position];
						}
						AddValue(opt, valueArg, locale);
					}
					else
						AddValue(opt, null, locale);

					position += 1;
				}
				else
				{
					otherArgs.Add(curArg);
					position += 1;
				}
			}
			for(; position < argv.Length; ++position)
				otherArgs.Add(argv[position]);

			this.remainingArgs = new string[otherArgs.Count];
			otherArgs.CopyTo(remainingArgs, 0); // eja wonders about the type mismatch...
		}


		private void AddValue(OptionBase opt, string valueArg, CultureInfo locale)
		{
			object value = opt.GetValue(valueArg, locale);
			string lf = opt.LongForm;

			IList<object> v = values[lf];

			if(v == null)
			{
				v = new List<object>();
				values[lf] = v;
			}

			v.Add(value);
		}


		private string[] remainingArgs = null;
		private IDictionary<string, OptionBase> options = new Dictionary<string, OptionBase>(10);
		private IDictionary<string, IList<object>> values = new Dictionary<string, IList<object>>(10);
	}

}
