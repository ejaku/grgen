package de.unika.ipd.grgen.util;

public class MutableInteger
{
	int value;
	
	public MutableInteger (int v) {
		value = v;
	}

	public int getValue()
    {
    	return value;
    }

	public void setValue(int value)
    {
    	this.value = value;
    }
}
