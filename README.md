# KSP-Recall

Recall for KSP blunders, screw ups and borks.

Aims to fix Stock misbehaviours the most seamlessly as possible, trying hard to **do not** introduce new ones in the process. :)


## In a Hurry

* Documentation	
	+ [Homepage](http://ksp.lisias.net/add-ons/KSP-Recall) on L Aerospace KSP Division
	+ [Project's README](https://github.com/net-lisias-ksp/KSP-Recall/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-ksp/KSP-Recall/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [Known Issues](./KNOWN_ISSUES.md)
	+ [TODO](./TODO.md) list
* Official Distribution Sites:
	+ [CurseForge](https://www.curseforge.com/kerbal/ksp-mods/ksp-recall)
	+ [SpaceDock](https://spacedock.info/mod/2434/KSP-Recall)
	+ [Latest Release](https://github.com/net-lisias-ksp/KSP-Recall/releases)
		- [Binaries](https://github.com/net-lisias-ksp/KSP-Recall/tree/Archive)
* Support
	+ [Homepage](http://ksp.lisias.net/add-ons/KSP-Recall/Support/) on L Aerospace KSP Division
	+ [Reddit](https://www.reddit.com/r/KSP_Recall/)
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-*)
	+ [Discussions](https://github.com/net-lisias-ksp/KSP-Recall/discussions)
* [Source](https://github.com/net-lisias-ksp/KSP-Recall)
	+ [Issue Tracker](https://github.com/net-lisias-ksp/KSP-Recall/issues)


## Description

Tired of constantly updating your Add'Ons each time a new KSP release fsck up something?

Restless while waiting your favorite Add'On to be updated so you can play without invoking devils using dark and incomprehensible rituals?

So this Add'On is for you.

By installing this thingy, unsolved bugs and mishaps from KSP Development Team will be fixed or at least worked around, saving Add'On Authors from the hassle to handle them themselves - most of the time without introducing new ones. :)

It aims to need minimal coupling with existent code, as well to be selectively injected on the affected parts in order to prevent *unholy interactions with third-party modules* that decide to fix things their own way.

Currently, the following fixes are available once installed:

* Parked crafts (even without wheels) drifting the Heading randomly on KSP >= 1.8
	+ Crafts with wheels also drifts, but due a different problem - still to be tackled down.
	+ This was discussed [here](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24321-2020-00804/&do=findComment&comment=3836931).
* Resources being reset to prefab when a part has his Resources changed by an Add'On (as TweakScale) (i.e., by Alt+Click a part, or by using symmetry) on KSP 1.9.x . 
	+ [KSP 1.9.x resets resources to prefab while cloning parts](https://github.com/net-lisias-ksp/KSP-Recall/issues/1)
		- This solution aims to be reusable to any Add'On that have the same problem with a simple two liner.
	+ Some Add'Ons that supports TweakScale by using `Scale_Redist.dll` are also fixed by collateral effect.
* Losing Resources' amount settings when cloning parts (Alt+Click) on KSP 1.9.x
	+ As described [here](https://forum.kerbalspaceprogram.com/index.php?/topic/193875-*).
* Editor mangling Surface Attached Parts' position when loading crafts.
	+ This was (properly this time) discussed [here](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-130/&do=findComment&comment=4066415). 
	+ Currently working only for KSP 1.9.x
* I.C.A. (Instantaneous Craft Annihilation) on KSP 1.11.0 when launching or switching back to vessels with some older parts, when the vessel is over a static with collider (does not happens when the craft is directly over the PQS ground)
* Seamless fix for Add'Ons that [implements `IPastCostModifier` running on KSP 1.11.x](https://github.com/net-lisias-ksp/KSP-Recall/issues/12)
	+ [Darth Pointer's Pay to Play](https://github.com/DarthPointer/PayToPlay/)
	+ [FreeThinker's Interstellar Fuel Switch](https://github.com/sswelm/KSP-Interstellar-Extended/)
	+ [allista's Cargo Accelerators](https://github.com/allista/CargoAccelerators)
	+ [All Angel 125 Add'Ons that uses WildBlueTools](https://github.com/Angel-125/WildBlueTools/)
	+ [Nathan Kell's Modular Fuel System](https://github.com/NathanKell/ModularFuelSystem/) (and Real Fuels)
	+ [IgorZ's Kerbal Inventory System](https://github.com/ihsoft/KIS)
	+ [KOS](https://github.com/KSP-KOS/KOS)
	+ [Kerbalism](https://github.com/Kerbalism/Kerbalism)
	+ [And many, many others](https://github.com/search?o=desc&p=3&q=IPartCostModifier&s=indexed&type=Code), including Squad's own modules (see bug [#26988](https://bugs.kerbalspaceprogram.com/issues/26988)):
		- PartStatsUpgradeModule
		- ModulePartVariants
		- ModuleProceduralFairings 
* More to come as a Needed to Code basis.

Fixes not needed on the current KSP instalment **are not applied**. So it's safe to just install KSP-Recall on anything you have (besides wasting a tiny little bit more time on the loading).


## For End Users

This is not intended to be "used" by end-users. It provides services to Add'On authors and/or fixes automatically some known problems on many different KSP versions.

But there're some options that will allow the end user to control how the fixes works on his/machine.

**Every fix** can be deactivated on a given part, without affecting the other ones. This change is persisted on the craft file and on the savegame, so you can even deactivate a fix on a part on a craft and not do it on another craft. For people willing to write patches, the name of the attribute is `active`.

The user can also force his/her hand on how and where the fixes is installed by editing `GameData/999_KSP-Recall/KSP-Recall.cfg`. Changing this file will overrule the default installation decisions KSP-Recall does on startup, allowing you use fixes that usually would not be available for your rig. **Use this with caution and prudence**, this can royally screw up your savegames.

Currently, the following automatic fixes are available:

### Automatically restores Resources changed by Fuel Switches.

As the title says, KSP-Recall detects when something changed on the craft (while Editing it) and tries to restore the Resources as intended by the Add'On authors, brute forcing the right way over the KSP 1.9.x. had brute forced its way (that breaks some Add'Ons).

However, and you as an user must be aware of it, some Fuel Switches don't cope very well with others. It's highly probable that you will have problems on installing more than on Fuel Switch on your KSP, because some of them install themselves on the parts without caring about any other one already there.

On a rule of thumb, it's possible to have more than one Fuel Switch installed on KSP. What you can't is have more than one Fuel Switch installed on the same part, and this is where most Fuel Switches authors are stomping their own toes.

Right now, this is what it's known to the date:

* Interstellar Fuel Switch
	+ Works without KSP-Recall.
	+ It coded its own Resource Management, so it's imune to KSP 1.9 "bruteness".
* Firespitter
	+ I detected no problems on it
* Anything that supports (or it's supported by) TweakScale
	+ TweakScale already handles KSP-Recall, so anything that makes use of `Scale_Redist` is already covered
	+ Modular Fuel Tanks is one of that Add'Ons, as long nobody shoves it on parts that already have another Fuel Switch.
* B9 Parts Switch should work. As long there're no other Fuel Switch installed on the same part.
* Other fuel switches:
	+ As long the OnEditorVesselModified is fired on each change (or one of the Custom Events from Recall, see below), it should work fine.
	+ Please report on this [Forum Thread](thread) or [Discussion](https://github.com/net-lisias-ksp/KSP-Recall/discussions) anything that you thing is wrong. I'm fixing them as I'm aware of them. If you are absolutely sure it's a bug, file a [bug report](https://github.com/net-lisias-ksp/KSP-Recall/issues).

### Crafts at rest changes drifts the Heading by their own

On TweakScale's [Forum thread](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24321-2020-00804/&do=findComment&comment=3836931), a discussion about a problem introduced on KSP 1.8 that made the crafts to drift the heading randomly when leaved at their own, even with parking breaks or not wheels at all!

KSP-Recall introduces a new work around for this problem, detecting when the Heading drift is unwanted and canceling it.

It **does not** fixes, specifically, the drift induced by the wheels itself, but helps on preventing situations where the wheels problem would be triggered.

### Fixing Costs refunds when Recovering Crafts on KSP 1.11.x

On TweakScale's [Forum thread](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*/&do=findComment&comment=3934078), a discussion about a problem introduced on KSP 1.1.0 that made refunds on craft recovering problematic - as the user end up losing any extra costs a module implements for the part using the `IPartCostModifier`.

Many, many add'ons are affected by this problem.

KSP-Recall solves this by implementing a new module, `Refunding`, that makes use of a meta-resource (also called `Refunding`) to trick the game cost recovering algorithms in giving you back the Funds it miscalculates.


## For Add'On Authors

### [Issue #1](https://github.com/net-lisias-ksp/KSP-Recall/issues/1) KSP 1.9.x resets resources to prefab while cloning parts

Add'Ons that supports TweakScale using `Scale_Redist.dll` but do not change resources themselves will be automatically fixed - TweakScale "calls" KSP Recall as the last step of the rescaling, anything you do on `IRescalable::OnRescale(ScalingFactor factor)` will be preserved.

Add'Ons that also changes the Part's resources outside of the `OnRescale` callback or without direct support for/from TweakScale will need to add the following lines of code every time their Part Resources are changed:

```C#
            // send Resource Changed message to KSP Recall if needed
            if (0 != this.part.Resources.Count)
            {
                BaseEventDetails data = new BaseEventDetails (BaseEventDetails.Sender.USER);
                data.Set<int> ("InstanceID", this.part.GetInstanceID());
                data.Set<Type>("issuer", this.GetType ());
                part.SendEvent ("OnPartResourceChanged", data, 0);
            }

// note: If your part can be set to have <ZERO> resources, omit the "if" above to hint Recall that it should delete any internal cache for the part
```

It's **extremely** important to send this event **only at the last moment possible**, after changing the resources. There's no guarantee about when this Event will be handled (it can be handled right on the spot, or only next week - nobody knows). You can only be sure about **when it will not be handled**: before being issued. So do whatever you need, and add that lines above before the end of the function/method/procedure/whatever to avoid risking Recall caching your resources before you are done.

_Add'Ons that support TweakScale by handling the `OnPartScaleChanged` event need to add that lines above too as the last step of their scaling tasks_. Since Events are completely asynchronous, there's a good chance that by the time TweakScale sends `OnPartResourceChanged` to Recall, you could not handled yet (or be in the middle of the handling!!!) your `OnPartScaleChanged` , and so Recall will essentially undo what you had done on your part (as it will get a snapshot of the current Resources before the change).


## Installation

Detailed installation instructions are now on its own file (see the [In a Hurry](#in-a-hurry) section) and on the distribution file.

### Licensing

* KSP-Recall is double licensed as follows:
	+ [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt). See [here](./LICENSE.KSPe.SKL-1_0)
		+ You are free to:
			- Use : unpack and use the material in any computer or device
			- Redistribute : redistribute the original package in any medium
		+ Under the following terms:
			- You agree to use the material only on (or to) KSP
			- You don't alter the package in any form or way (but you can embedded it)
			- You don't change the material in any way, and retain any copyright notices
			- You must explicitly state the author's Copyright, as well an Official Site for downloading the original and new versions (the one you used to download is good enough)
	+ [GPL 2.0](https://www.gnu.org/licenses/gpl-2.0.txt). See [here](./LICENSE.KSPe.GPL-2_0)
		+ You are free to:
			- Use : unpack and use the material in any computer or device
			- Redistribute : redistribute the original package in any medium
			- Adapt : Reuse, modify or incorporate source code into your works (and redistribute it!) 
		+ Under the following terms:
			- You retain any copyright notices
			- You recognise and respect any trademarks
			- You don't impersonate the authors, neither redistribute a derivative that could be misrepresented as theirs.
			- You credit the author and republish the copyright notices on your works where the code is used.
			- You relicense (and fully comply) your works using GPL 2.0
				- Please note that upgrading the license to GPLv3 **IS NOT ALLOWED** for this work, as the author **DID NOT** added the "or (at your option) any later version" on the license.
			- You don't mix your work with GPL incompatible works.
	* If by some reason the GPL would be invalid for you, rest assured that you still retain the right to Use the Work under SKL 1.0. 

Please note the copyrights and trademarks in [NOTICE](./NOTICE).


## UPSTREAM

There's no upstream, **I am (g)ROOT** :)
