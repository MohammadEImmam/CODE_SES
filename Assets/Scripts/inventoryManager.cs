using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class inventoryManager {
	public static bool[] isItemAvailable = new bool[4];

	public static bool getItem(int index) {
		try {
			return isItemAvailable[index];
		}
		catch (IndexOutOfRangeException e){
			throw new IndexOutOfRangeException("index out of range", e);
		}
	}

	public static void setItem(int index, bool val) {
		try {
			isItemAvailable[index] = val;
		}
		catch (IndexOutOfRangeException e){
			throw new IndexOutOfRangeException("index out of range", e);
		}
	}
}