using Terraria.ModLoader;

namespace PortableStorageFluids;

public class PortableStorageFluids : Mod
{
	public override void Load()
	{
		Hooking.Hooking.Load();
	}
}