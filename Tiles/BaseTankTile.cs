using BaseLibrary.Utility;
using FluidLibrary;
using PortableStorageFluids.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace PortableStorageFluids.Tiles;

public abstract class BaseTankTile : ModTile
{
	public override void PlaceInWorld(int i, int j, Item item)
	{
		if (!TileEntityUtility.TryGetTileEntity(Player.tileTargetX, Player.tileTargetY, out TileEntities.BaseTankTileEntity tileEntity))
			return;

		if (item.ModItem is not BaseTank tank)
			return;

		FluidStack teFluid = tileEntity!.GetFluidStorage()[0];
		FluidStack itemFluid = tank.GetFluidStorage()[0];

		teFluid.Fluid = itemFluid.Fluid;
		teFluid.Volume = itemFluid.Volume;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		if (!TileEntityUtility.TryGetTileEntity(i, j, out TileEntities.BaseTankTileEntity tileEntity))
			return;

		// TileObjectData data = TileObjectData.GetTileData(tileEntity.TileType, 0);
		int k = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, -ItemDrop);

		if (Main.item[k].ModItem is BaseTank item)
		{
			FluidStack teFluid = tileEntity!.GetFluidStorage()[0];
			FluidStack itemFluid = item.GetFluidStorage()[0];

			itemFluid.Fluid = teFluid.Fluid;
			itemFluid.Volume = teFluid.Volume;
		}

		tileEntity!.Kill(i, j);
	}
}