﻿using UnityEngine;
using System.Collections;

public class LevelGenerator:Singleton<LevelGenerator> {
	private static LevelGenerator instance = null;

	public Object[] defaultRoomContent;
	public Object[] hallRoomContent;
	public Object[] cornerRoomContent;
	public Object[] TroomRoomContent;
	public Object[] plusRoomContent;
	public Object[] bigRoomContent;

	public Object[] oneSideChunk;
	public Object[] twoSideChunk;
	public Object[] threeSideChunk;
	public Object[] fourSideChunk;

	public Texture currentMap;

	void Start() {
		defaultRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Default");
		hallRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Hall");
		cornerRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Corner");
		TroomRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Troom");
		plusRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/Plus");
		bigRoomContent = Resources.LoadAll("Prefabs/Rooms/Content/BigRoom");

		oneSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/1Side");
		twoSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/2Side");
		threeSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/3Side");
		fourSideChunk = Resources.LoadAll("Prefabs/Rooms/Chunks/4Side");
	}
}