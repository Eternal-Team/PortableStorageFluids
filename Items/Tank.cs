using FluidLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortableStorageFluids.Items;

public class Tank : BaseTank
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Tank";

	public override void OnCreate(ItemCreationContext context)
	{
		base.OnCreate(context);

		Storage = new FluidStorage(255 * 1000);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.width = 40;
		Item.height = 48;
		Item.value = Item.buyPrice(gold: 10);
		Item.rare = ItemRarityID.Red;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.LunarBar, 3)
			.AddIngredient(ItemID.SlimeBlock, 50)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}