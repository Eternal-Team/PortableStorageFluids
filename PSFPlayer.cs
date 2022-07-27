using PortableStorageFluids.Items;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace PortableStorageFluids;

public class KeybindSystem : ModSystem
{
	public static ModKeybind SwitchModeKeybind { get; private set; }

	public override void Load()
	{
		SwitchModeKeybind = KeybindLoader.RegisterKeybind(Mod, "Switch Mode", "M");
	}

	public override void Unload()
	{
		SwitchModeKeybind = null;
	}
}

public class PSFPlayer : ModPlayer
{
	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		if (KeybindSystem.SwitchModeKeybind.JustPressed)
		{
			if (Player.HeldItem.ModItem is BaseTank tank)
			{
				tank.SwitchBucketMode();
			}
		}
	}
}