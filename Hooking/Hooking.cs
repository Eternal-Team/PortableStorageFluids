using System;
using System.Collections.Generic;
using System.Linq;
using FluidLibrary;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using PortableStorageFluids.Items;
using Terraria;

namespace PortableStorageFluids.Hooking;

public static class Hooking
{
	public static void Load()
	{
		IL.Terraria.Player.AdjTiles += AdjTiles;
	}

	private static IEnumerable<BaseTank> GetTanks(Player player)
	{
		foreach (Item item in player.inventory)
		{
			if (item.IsAir) continue;

			if (item.ModItem is BaseTank tank) yield return tank;
		}
	}

	private static void AdjTiles(ILContext il)
	{
		ILCursor cursor = new ILCursor(il);

		if (cursor.TryGotoNext(i => i.MatchLdsfld<Main>("playerInventory")))
		{
			cursor.Emit(OpCodes.Ldarg, 0);

			cursor.EmitDelegate<Action<Player>>(player =>
			{
				foreach (BaseTank tank in GetTanks(player))
				{
					if (tank.GetFluidStorage().Any(fluid => fluid.Fluid is Water)) player.adjWater = true;
					if (tank.GetFluidStorage().Any(fluid => fluid.Fluid is Lava)) player.adjLava = true;
					if (tank.GetFluidStorage().Any(fluid => fluid.Fluid is Honey)) player.adjHoney = true;
				}
			});
		}
	}
}