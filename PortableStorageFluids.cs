using Terraria.ModLoader;

namespace PortableStorageFluids;

public class PortableStorageFluids : Mod
{
	public const string AssetPath = "PortableStorageFluids/Assets/";

	public override void Load()
	{
		Hooking.Hooking.Load();
	}
}