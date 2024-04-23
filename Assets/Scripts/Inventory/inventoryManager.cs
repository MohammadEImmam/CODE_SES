using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using InGameCodeEditor;

public static class inventoryManager {
	// manages player access to items. get/set are called on item purchase from shop
	public static bool[] isItemAvailable = new bool[8];

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

			if(index < 4) {

			}
		}
		catch (IndexOutOfRangeException e){
			throw new IndexOutOfRangeException("index out of range", e);
		}
	}
}