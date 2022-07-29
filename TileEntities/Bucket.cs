using System;
using FluidLibrary;

namespace PortableStorageFluids.TileEntities;

public class Bucket : BaseTankTileEntity
{
	protected override Type TileType => typeof(Tiles.Bucket);

	public Bucket()
	{
		Storage = new FluidStorage(255 * 10);
	}
}