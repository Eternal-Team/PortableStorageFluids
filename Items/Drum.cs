using FluidLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortableStorageFluids.Items;

public class Drum : BaseTank
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Drum";

	protected override int TileType => ModContent.TileType<Tiles.Drum>();
	
	public override void OnCreate(ItemCreationContext context)
	{
		base.OnCreate(context);

		Storage = new FluidStorage(255 * 100);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.width = 24;
		Item.height = 40;
		Item.value = Item.buyPrice(gold: 1);
		Item.rare = ItemRarityID.Pink;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.MythrilBar, 10)
			.AddIngredient(ItemID.SlimeBlock, 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.CobaltBar, 10)
			.AddIngredient(ItemID.SlimeBlock, 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}