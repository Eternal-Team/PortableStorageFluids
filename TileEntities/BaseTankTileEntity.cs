using System;
using BaseLibrary;
using FluidLibrary;
using Terraria.ModLoader.IO;

namespace PortableStorageFluids.TileEntities;

public abstract class BaseTankTileEntity : BaseTileEntity, IFluidStorage
{
	private Guid UUID;
	protected FluidStorage Storage;

	public BaseTankTileEntity()
	{
		UUID = Guid.NewGuid();
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

	public FluidStorage GetFluidStorage() => Storage;
}