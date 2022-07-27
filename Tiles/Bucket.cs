using BaseLibrary.Utility;
using PortableStorageFluids.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PortableStorageFluids.Tiles;

public class Bucket : ModTile
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Bucket";

	public override void SetStaticDefaults()
	{
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TileEntities.Bucket>().Hook_AfterPlacement, -1, 0, false);

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

	// private static Asset<Texture2D> effect = ModContent.Request<Texture2D>(Teleportation.AssetPath + "Textures/Tiles/TeleporterEffect");
	//
	// public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	// {
	// 	if (!TileEntityUtility.TryGetTileEntity(i, j, out TileEntities.Teleporter teleporter)) return true;
	// 	if (teleporter == null || !Main.tile[i, j].IsTopLeft() || !teleporter.Active) return true;
	//
	// 	Vector2 position = new Point16(i + 1, j).ToScreenCoordinates();
	//
	// 	spriteBatch.Draw(effect.Value, position + new Vector2(8, 2), null, Color.White * 0.75f, 0f, new Vector2(10, 100), new Vector2(2, 1), SpriteEffects.None, 0f);
	//
	// 	return true;
	// }
	//
	// public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	// {
	// 	if (!TileEntityUtility.TryGetTileEntity(i, j, out TileEntities.Teleporter teleporter)) return;
	//
	// 	if (teleporter.Active)
	// 	{
	// 		r = 1.0f;
	// 		g = 0.863f;
	// 		b = 0.0f;
	// 	}
	// 	else
	// 	{
	// 		r = 0.2f;
	// 		g = 0.2f;
	// 		b = 0.8f;
	// 	}
	// }
	//
	// public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	// {
	// 	if (drawData.tileFrameX % 18 == 0 && drawData.tileFrameY % 18 == 0)
	// 	{
	// 		Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
	// 	}
	// }
	//
	// public override bool RightClick(int i, int j)
	// {
	// 	if (!TileEntityUtility.TryGetTileEntity(i, j, out TileEntities.Teleporter teleporter)) return false;
	//
	// 	PanelUI.Instance.HandleUI(teleporter);
	//
	// 	return true;
	// }

	public override void PlaceInWorld(int i, int j, Item item)
	{
		if (!TileEntityUtility.TryGetTileEntity(Player.tileTargetX, Player.tileTargetY, out TileEntities.Bucket tileEntity))
			return;
			
		tileEntity.GetFluidStorage()[0].Fluid = (item.ModItem as BaseTank).GetFluidStorage()[0].Fluid;
		tileEntity.GetFluidStorage()[0].Volume = (item.ModItem as BaseTank).GetFluidStorage()[0].Volume;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		if (!TileEntityUtility.TryGetTileEntity(i, j, out TileEntities.Bucket bucket)) return;

		int k = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Bucket>());
		if (Main.item[k].ModItem is BaseTank tank)
		{
			tank.GetFluidStorage()[0].Fluid = bucket.GetFluidStorage()[0].Fluid;
			tank.GetFluidStorage()[0].Volume = bucket.GetFluidStorage()[0].Volume;
		}

		bucket.Kill(i, j);
	}
}