package model;

import java.io.Serializable;
import java.util.ArrayList;

@SuppressWarnings("serial")
public class Sheet implements Serializable {

	private ArrayList<Integer> choices = new ArrayList<Integer>();
	
	public void setChoices(ArrayList<Integer> choices) {
		this.choices = choices;
	}
	
	public ArrayList<Integer> getChoices() {
		return this.choices;
	}
}
