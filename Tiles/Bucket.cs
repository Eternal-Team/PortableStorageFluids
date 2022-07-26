﻿using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PortableStorageFluids.Tiles;

public class Bucket : BaseTankTile
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Bucket";

	public override void SetStaticDefaults()
	{
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TileEntities.Bucket>().Hook_AfterPlacement, -1, 0, false);
		ItemDrop = -ModContent.ItemType<Items.Bucket>();

		Main.tileSolid[Type] = false;
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = false;
		Main.tileLavaDeath[Type] = false;
		Main.tileHammer[Type] = false;
		Main.tileLighted[Type] = true;

		var newTile = TileObjectData.newTile;
		newTile.Width = 2;
		newTile.Height = 2;
		newTile.Origin = new Point16(0, 0);
		newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, newTile.Width, 0);
		newTile.UsesCustomCanPlace = true;
		newTile.CoordinateHeights = new[] { 16, 16 };
		newTile.CoordinateWidth = 16;
		newTile.CoordinatePadding = 2;
		TileObjectData.addTile(Type);
	}
}