using System;
using FluidLibrary;

namespace PortableStorageFluids.TileEntities;

public class Drum : BaseTankTileEntity
{
	protected override Type TileType => typeof(Tiles.Drum);

	public Drum()
	{
		Storage = new FluidStorage(255 * 100);
	}
}