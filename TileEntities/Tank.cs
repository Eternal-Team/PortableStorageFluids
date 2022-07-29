using System;
using FluidLibrary;

namespace PortableStorageFluids.TileEntities;

public class Tank : BaseTankTileEntity
{
	protected override Type TileType => typeof(Tiles.Tank);

	public Tank()
	{
		Storage = new FluidStorage(255 * 1000);
	}
}