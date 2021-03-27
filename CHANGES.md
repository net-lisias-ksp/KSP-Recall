# KSP-Recall :: Changes

* 2021-0327: 0.0.7.7 (LisiasT) for KSP >= 1.8.0
	+ Properly implemented the `active` attribute, allowing `Refunding` to be deactivate on a part per part basis if something wrong happens on the field.
	+ Reworked the `Refunding` mechanism, injecting the Resource at runtime as needed.
		- This will hopefully workaround the problem with Fuel Switches and other add'ons that mangle the Resource Pool of the part.
	+ Preventing `Resourceful` to be applied on parts with `ModuleCargoPart`, `ModuleComet`, `ModuleAsteroid` and `ModuleKerbal` as these are proven problematic on the field with code that handles resources. 
	+ The `activate` property of all Modules are now persisted on saving.
		- Be cautious, this can screw up your savegame. 
* 2021-0322: 0.0.7.6 (LisiasT) for KSP >= 1.8.0
	+ Made the `Refunding` stunt invisible on the U.I., decluttering the widgets.
		- Thanks for the [tip](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-18/&do=findComment&comment=3943384), [Hohmannson](https://forum.kerbalspaceprogram.com/index.php?/profile/202712-hohmannson/)
	+ Preventing `Refunding` from messing up Stock Stackable parts
		- At the expense of having its costs fixed on Recovering, however
		- Thanks for [Krazy1](https://forum.kerbalspaceprogram.com/index.php?/profile/203523-krazy1/) for reporting [this](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-18/&do=findComment&comment=3943444).
	+ Preventing `Refunding` from messing up Stackable on KIS
		- No collateral effects expected on this one.
	+ Added a Sanity Check for `Refunding` on KSP-Recall Startup. 
* 2021-0308: 0.0.7.5 (LisiasT) for KSP >= 1.8.0
	+ Fixed a pretty lame mistake on initiating the `Refund` PartModule.
* 2021-0305: 0.0.7.3 (LisiasT) for KSP >= 1.8.0
	+ (Properly) Implements a ~~ugly hack~~, I mean, a workaround for the KSP 1.11.x bug on recovering funds described on Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12).
		- Thanks a lot to [firethorn6](https://forum.kerbalspaceprogram.com/index.php?/profile/210389-firethorn6/) and [DarthPointer](https://forum.kerbalspaceprogram.com/index.php?/profile/203932-darthpointer/) for reporting the problem and further help on diagnosing it and testing the solution!
	+ Fixes a deployment mishap for CurseForge
* 2021-0303: 0.0.7.2 **BETA** (LisiasT) for KSP >= 1.8.0
	+ Enhances further more that ~~ugly hack~~, I mean, a workaround for the KSP 1.11.x bug on recovering funds described on Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12).
		- Most, if not all, Add'Ons are expected to be supported directly or indirectly this time. 
	+ **Attention please**
		- **DO NOT** use this on "production". This thing may be unsafe, as I used some dirty tricks that can backfire later.
* 2021-0303: 0.0.7.1 **BETA** (LisiasT) for KSP >= 1.8.0
	+ **DITCHED** due a mishap on the craft charge on Launch 
* 2021-0302: 0.0.7.0 **BETA** (LisiasT) for KSP >= 1.8.0
	+ Implements a ~~ugly hack~~, I mean, a workaround for the KSP 1.11.x bug on recovering funds described on Issue [#12](https://github.com/net-lisias-ksp/KSP-Recall/issues/12).
	+ **Attention please**
		- **DO NOT** use this on "production". This thing may be unsafe, as I used some dirty tricks that can backfire later.
		- Not all add'ons are guaranteed to work yet, I need to study some affected add'ons in order to detect the most simple way to support them. 
* 2021-0209: 0.0.6.1 (LisiasT) for KSP >= 1.8.0
	+ Updating the KSPe.Light, with a fix on the installment check
	+ Some minor fixes on the stats of the Sanity Checks.
	+ Removing the Beta status, the `ChilliongOut` stunt appears to be working fine.
* 2021-0106: 0.0.6.0 **BETA** (LisiasT) for KSP >= 1.8.0
	+ Preliminary attempt to overcome the new bug on launching from KSP 1.11
	+ Check the following posts for more information:
		- [KAX](https://forum.kerbalspaceprogram.com/index.php?/topic/180268-131/page/9/&tab=comments#comment-3901075)
		- [Impossible Innovations](https://forum.kerbalspaceprogram.com/index.php?/topic/175694-131/&do=findComment&comment=3901072).  
* 2020-1220: 0.0.5.0 (LisiasT) for KSP >= 1.8.0
	+ Preventing installing Driftless on KSP 1.11.
		- Check [Issue #10](https://github.com/net-lisias-ksp/KSP-Recall/issues/10)
* 2020-0827: 0.0.4.4 (LisiasT) for KSP >= 1.8.0
	+ Some tool (and I'm hunting this \*\*\*\*), with all its wisdom, decided to "help me" adding silently a configuration that automatically converts EoL between UNIX and Windows. And did the stunt on a DLL.
		+ This release fixes the DLL.
		+ Full history on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24321-2020-00804/&do=findComment&comment=3845367). 
* 2020-0817: 0.0.4.3 (LisiasT) for KSP >= 1.8.0
	+ Fixes an annoying situation where Decouplers and Docking Ports with `Driftless` blocks fuel to engines above them on the stack. 
* 2020-0817: 0.0.4.1 (LisiasT) for KSP >= 1.8.0
	+ An error on handling parts without `RigidBody` was raining NREs on the KSP.log. Fixed.
	+ A slightly smarter handling of inactive and rigidbodyless parts may save a tiny little bit of CPU time.
	+ Specialised treatment for Kerbals on EVA, as it was realised that Kerbals drifts a lot more than crafts - by reasons still unknown at this moment.
* 2020-0815: 0.0.4.0 (LisiasT) for KSP >= 1.8.0
	+ Adds a Work Around for crafts drifting on the Heading at rest, even when without wheels attached.
		- There's another similar problem on the wheels themselves, KSP Recall are still working on this one
	+ **Way** smarter selective applying of the Modules when needed.  
