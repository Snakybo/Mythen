﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ItemManager:EditorWindow {
	private ItemList itemList;
	private ItemCreator itemCreator;
	
	private GameObject selectedGameObject;
	
	[MenuItem("Window/Item Manager")]
	static void Init() {
		ItemManager itemManager = (ItemManager)EditorWindow.GetWindow(typeof(ItemManager));
		itemManager.title = "Item Manager";
		
		itemManager.Show();
		itemManager.Focus();
	}
	
	void OnGUI() {
		if(!CheckForItemList())
			return;
		
		if(itemCreator != null)
			if(itemCreator.Updated)
				this.Repaint();
	
		if(GUILayout.Button("Add items"))
			AddItemCreator();
			
		DrawEquipableItems();
		DrawPowerItems();
		DrawSpecialItems();
	}
	
	/** Draw all equipable items (except the weapons) */
	private void DrawEquipableItems() {
		GUILayout.Label("Equipable Items", EditorStyles.boldLabel);

		GUIStyle style = new GUIStyle();
		style.fontStyle = FontStyle.Bold;
		style.margin = new RectOffset(5, 4, 8, 4);
		
		foreach(ItemEquipable item in itemList.EquipableItems) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.Model);

			style.normal.textColor = ItemEquipable.RarityColor(item.Rarity);

			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));
				
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, style, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							if(itemCreator == null)
								AddItemCreator();
								
							//itemCreator.EditEquipable(item);
						}
					EditorGUILayout.EndHorizontal();
					
					GUILayout.Label(item.Element.ToString() + " " + item.TypeEquipable.ToString(), GUILayout.Width(225f));
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.Stats.Speed.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Defence: " + item.Stats.Defence.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Damage: " + item.Stats.Damage.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Store price: " + item.Stats.StorePrice.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
		
		DrawWeaponItems();
	}
	
	/** Draw all weapons */
	private void DrawWeaponItems() {
		foreach(ItemWeapon item in itemList.WeaponItems) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.Model);
			
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));
				
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							if(itemCreator == null)
								AddItemCreator();
							
							//itemCreator.EditWeapon(item);
						}
					EditorGUILayout.EndHorizontal();
					
					GUILayout.Label(item.Element.ToString() + " " + item.TypeEquipable.ToString() + " - " + item.TypeWeapon.ToString(), GUILayout.Width(225f));
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.Stats.Speed.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Defence: " + item.Stats.Defence.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Damage: " + item.Stats.Damage.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Store price: " + item.Stats.StorePrice.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	/** Draw all Power items */
	private void DrawPowerItems() {
		GUILayout.Label("Power Items", EditorStyles.boldLabel);
	
		GUIStyle style = new GUIStyle();
		style.margin = new RectOffset(65, 0, 0, 0);
		
		foreach(ItemPower item in itemList.PowerItems) {
			EditorGUILayout.BeginHorizontal(style);
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							if(itemCreator == null)
								AddItemCreator();
							
							//itemCreator.EditPower(item);
						}
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Type: " + item.TypePower.ToString(), GUILayout.Width(205f));
						GUILayout.Label("Time: " + item.Time.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	/** Draw all Special Items */
	private void DrawSpecialItems() {
		GUILayout.Label("Special Items", EditorStyles.boldLabel);
	
		GUIStyle style = new GUIStyle();
		style.margin = new RectOffset(65, 0, 0, 0);
		
		foreach(ItemSpecial item in itemList.SpecialItems) {
			EditorGUILayout.BeginHorizontal(style);
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							if(itemCreator == null)
								AddItemCreator();
							
							//itemCreator.EditSpecial(item);
						}
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("ID: " + item.Id.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	/** Create the Item Creator */
	private void AddItemCreator() {
		itemCreator = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));
		
		itemCreator.title = "Item Creator";
		
		itemCreator.Show();
		itemCreator.Focus();
	}
	
	/** Check if the selected Game Object has an ItemList component attached */
	private bool CheckForItemList() {
		bool found = true;
	
		if(Selection.activeGameObject == null || (Selection.activeGameObject != selectedGameObject && selectedGameObject != null)) {
			GUILayout.Label("No Game Object with an ItemList component selected");
			found = false;
		} else if(itemList == null) {
			itemList = (Selection.activeGameObject).GetComponent<ItemList>();
			
			if(itemList == null) {
				GUILayout.Label("Can't find an ItemList component on the selected Game Object");
				found = false;
			}
		}
		
		selectedGameObject = Selection.activeGameObject;
		
		return found;
	}
}
