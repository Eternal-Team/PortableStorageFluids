using FluidLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortableStorageFluids.Items;

public class Drum : BaseTank
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Drum";

	public override void OnCreate(ItemCreationContext context)
	{
		base.OnCreate(context);

		Storage = new FluidStorage(255 * 100);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.width = 28;
		Item.height = 28;
		Item.value = Item.buyPrice(gold: 5);
		Item.rare = ItemRarityID.Blue;
	}
}