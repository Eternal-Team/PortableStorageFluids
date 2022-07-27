using FluidLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortableStorageFluids.Items;

public class Bucket : BaseTank
{
	public override string Texture => PortableStorageFluids.AssetPath + "Textures/Bucket";

	public override void OnCreate(ItemCreationContext context)
	{
		base.OnCreate(context);

		Storage = new FluidStorage(255 * 10);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.width = 22;
		Item.height = 28;
		Item.value = Item.buyPrice(silver: 1);
		Item.rare = ItemRarityID.Blue;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup(RecipeGroupID.Wood, 7)
			.AddRecipeGroup(RecipeGroupID.IronBar, 3)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}