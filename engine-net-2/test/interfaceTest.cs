//#define TEST_FIRST

interface IA
{
	string val { get; set; }
}

interface IAI : IA
{
	new int val { get; set; }
}

#if TEST_FIRST

class AI : IAI
{
	public int val { get { return 42; } set {} }
	string IA.val { get { return "13"; } set {} }

	public void stuff(AI other)
	{
		int x = 7;
		other.val = x;
		val = x;
		x = other.val;
		x = val;
		val = other.val;
	}

	public void stuff2(IAI other)
	{
		int x = 7;
		other.val = x;
		val = x;
		x = other.val;
		x = val;
		val = other.val;
	}
}

#else

interface IAI2 : IAI { }

class AI2 : IAI2
{
	public int val { get { return 42; } set { } }
	string IA.val { get { return "13"; } set { } }

	public void stuff(AI2 other)
	{
		int x = 7;
		other.val = x;
		val = x;
		x = other.val;
		x = val;
		val = other.val;
	}

	public void stuff2(IAI2 other)
	{
		int x = 7;
		other.val = x;
		val = x;
		x = other.val;
		x = val;
		val = other.val;
	}
}

#endif

class MainClass { public static void Main() { } }
