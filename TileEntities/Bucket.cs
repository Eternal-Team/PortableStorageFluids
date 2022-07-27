using System;
using BaseLibrary;
using FluidLibrary;
using Terraria.ModLoader.IO;

namespace PortableStorageFluids.TileEntities;

public class Bucket : BaseTileEntity, /*IHasUI,*/ IFluidStorage
{
	protected override Type TileType => typeof(Tiles.Bucket);

	private Guid UUID;
	protected FluidStorage Storage;

	public Bucket()
	{
		UUID = Guid.NewGuid();

		Storage = new FluidStorage(255 * 10);
	}

	public override void OnPlace()
	{
	}

	public override void SaveData(TagCompound tag)
	{
		tag["UUID"] = UUID;
		tag["Fluids"] = Storage.Save();
	}

	public override void LoadData(TagCompound tag)
	{
		UUID = tag.Get<Guid>("UUID");
		Storage.Load(tag.GetCompound("Fluids"));
	}

	public override void OnKill()
	{
	}

	public FluidStorage GetFluidStorage() => Storage;

	// public Guid GetID() => UUID;
	//
	// public SoundStyle? GetCloseSound() => SoundID.Item1;
	//
	// public SoundStyle? GetOpenSound() => SoundID.Item1;
}