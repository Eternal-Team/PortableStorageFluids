using System;
using System.Collections.Generic;
using BaseLibrary;
using BaseLibrary.UI;
using BaseLibrary.Utility;
using FluidLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace PortableStorageFluids.Items;

public abstract class BaseTank : BaseItem, ICraftingStorage, IHasUI
{
	// public SoundStyle? OpenSound => new SoundStyle("PortableStorage/Assets/Sounds/BagOpen");
	// public SoundStyle? CloseSound => new SoundStyle("PortableStorage/Assets/Sounds/BagClose");

	public Guid ID;
	protected FluidStorage storage;
	
	protected abstract int TileType { get; }

	private bool bucketMode;
	public bool BucketMode
	{
		get => bucketMode;
		set
		{
			if (!value)
			{
				Item.createTile = TileType;
				Item.consumable = true;
			
				Item.ClearNameOverride();
			}
			else
			{
				Item.createTile = -1;
				Item.consumable = false;
				
				Item.SetNameOverride(Lang.GetItemNameValue(Item.type) + " (Bucket)");
			}
			
			bucketMode = value;
		}
	}

	protected FluidStorage Storage
	{
		set => storage = value;
		get
		{
			if (storage == null) OnCreate(null);
			return storage;
		}
	}

	public override ModItem Clone(Item item)
	{
		BaseTank clone = (BaseTank)base.Clone(item);
		clone.Storage = Storage.Clone();
		clone.BucketMode = BucketMode;
		clone.ID = ID;
		return clone;
	}

	public override void OnCreate(ItemCreationContext context)
	{
		ID = Guid.NewGuid();
		BucketMode = false;
	}

	public override void SetStaticDefaults()
	{
		SacrificeTotal = 1;
	}

	public override void SetDefaults()
	{
		Item.useTime = 5;
		Item.useAnimation = 5;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.rare = ItemRarityID.White;
		Item.autoReuse = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		FluidStack fluidStack = storage[0];
		string text = fluidStack?.Fluid != null ? Language.GetText("Mods.PortableStorageFluids.TankTooltip").Format(fluidStack.Volume / 255f, storage.MaxVolumeFor(0) / 255f, fluidStack.Fluid.DisplayName.Get()) : Language.GetText("Mods.PortableStorageFluids.TankTooltipEmpty").ToString();

		tooltips.Add(new TooltipLine(Mod, "PortableStorageFluids:TankTooltip", text));
	}

	public override bool ConsumeItem(Player player) => !BucketMode;

	public override bool AltFunctionUse(Player player) => true;

	public void SwitchBucketMode()
	{
		BucketMode = !BucketMode;

		Main.NewText(BucketMode ? "Bucket Mode: On" : "Bucket Mode: Off");
	}
	
	public override bool? UseItem(Player player)
	{
		if (!BucketMode)
			return true;

		int targetX = Player.tileTargetX;
		int targetY = Player.tileTargetY;
		Tile tile = Main.tile[targetX, targetY];

		// place
		if (player.altFunctionUse == 2)
		{
			if (tile.HasUnactuatedTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType] && tile.TileType != 546)
				return false;

			var fluidStack = storage[0];
			if (fluidStack.Fluid is null)
				return false;

			if (tile.LiquidAmount != 0 && tile.LiquidType != fluidStack.Fluid.Type)
				return false;

			if (!storage.RemoveFluid(player, 0, out FluidStack fluid, byte.MaxValue - tile.LiquidAmount))
				return false;

			SoundEngine.PlaySound(SoundID.Splash, player.position);

			tile.LiquidType = fluidStack.Fluid?.Type ?? 0;
			tile.LiquidAmount += (byte)fluid.Volume;

			WorldGen.SquareTileFrame(targetX, targetY);

			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.sendWater(targetX, targetY);
		}
		// remove
		else
		{
			if (tile.LiquidAmount <= 0)
				return false;

			FluidStack stack = new FluidStack(FluidLoader.GetFluid(tile.LiquidType), tile.LiquidAmount);
			if (!storage.InsertFluid(player, 0, ref stack))
				return false;

			SoundEngine.PlaySound(SoundID.Splash, player.position);

			byte remaining = (byte)stack.Volume;
			tile.LiquidAmount = remaining;
			if (remaining <= 0) tile.LiquidType = LiquidID.Water;

			WorldGen.SquareTileFrame(targetX, targetY, false);

			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.sendWater(targetX, targetY);
			else
				Liquid.AddWater(targetX, targetY);
		}

		return true;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Set("ID", ID);
		tag.Set("Fluids", Storage.Save());
		tag.Set("BucketMode", BucketMode);
	}

	public override void LoadData(TagCompound tag)
	{
		ID = tag.Get<Guid>("ID");
		Storage.Load(tag.Get<TagCompound>("Fluids"));
		BucketMode = tag.GetBool("BucketMode");
	}

	public FluidStorage GetFluidStorage() => Storage;

	public IEnumerable<int> GetTanksForCrafting()
	{
		yield return 0;
	}

	public Guid GetID() => ID;
}