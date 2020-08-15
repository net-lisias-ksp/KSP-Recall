# KSP-Recall

Recall for KSP blunders, screw ups and borks.

Aims to fix Stock misbehaviours the most seamlessly as possible, trying hard to introduce new ones in the process.


## In a Hurry

* [Latest Release](https://github.com/net-lisias-ksp/KSP-Recall/releases)
	+ [Binaries](https://github.com/net-lisias-ksp/KSP-Recall/tree/Archive)
* [Source](https://github.com/net-lisias-ksp/KSP-Recall)
* [Issue Tracker](https://github.com/net-lisias-ksp/KSP-Recall/issues)
* Documentation	
	+ [Homepage](http://ksp.lisias.net/add-ons/KSP-Recall) on L Aerospace KSP Division
	+ [Project's README](https://github.com/net-lisias-ksp/KSP-Recall/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-ksp/KSP-Recall/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [Known Issues](./KNOWN_ISSUES.md)
	+ [TODO](./TODO.md) list
* Official Distribution Sites:
	+ [Homepage](http://ksp.lisias.net/add-ons/KSP-Recall) on L Aerospace
	+ [Source and Binaries](https://github.com/net-lisias-ksp/KSP-Recall) on GitHub.


## Description

Tired of constantly updating your Add'Ons each time a new KSP release fsck up something?

Restless while waiting your favorite Add'On to be updated so you can play without invoking devils using dark and incomprehensible rituals?

So this Add'On is for you.

By installing this thingy, unsolved bugs and mishaps from KSP Development Team will be fixed or at least worked around, saving Add'On Authors from the hassle to handle them themselves - most o the time without introducing new ones. :)

It aims to need minimal coupling with existent code, as well to be selectively injected on the broken parts in order to prevent *unholly intercations with third-party modules* that decide to fix things their own way.

Currently, the following fixes are available once installed:

* TweakScale:
	+ KSP 1.9.x resets resources to prefab while cloning parts [#96](https://github.com/net-lisias-ksp/TweakScale/issues/96)
* VAP/SPH Editor
	+ When cloning parts (using or not symmetry), KSP 1.9.x "forgets" the amount of Resources set by the user. KSP Recall fixes that.
* Fuel Switches
	+ **Some** Fuel Switches suffers from the same problem as TweakScale above. They are also fixed by KSP-Recall
* Heading Drifting on crafts at rest
	+ From KSP 1.8 and above, crafts started to change the Heading at their own, even with parking breaks or no wheels at all!
	+ KSP-Recall now have a work around for this.
* More to come as a Need to Code basis.


## For End Users

This is not intended to be "used" by end-users. It provides services to Add'On authors and/or fixes automatically some known problems on many different KSP versions.

Currently, the following automatic fixes are available:

### Automatically restores Resources changed by Fuel Switches.

As the title says, KSP-Recall detects when something changed on the craft (while Editing it) and tries to restore the Resources as intended by the Add'On authors, brute forcing the right way over the KSP 1.9.x. had brute forced its way (that breaks some Add'Ons).

However, and you as an user must be aware of it, some Fuel Switches don't cope very well with others. It's highly probable that you will have problems on installing more than on Fuel Switch on your KSP, because some of them install themselves on the parts without caring about any other one already there.

On a rule of thumb, it's possible to have more than one Fuel Switch installed on KSP. What you can't is have more than one Fuel Switch installed on the same part, and this is where most Fuel Switches authors are stomping their own toes.

Right now, this is what it's known to the date:

* Interstellar Fuel Switch
	+ Works without KSP-Recall.
	+ It's coded it's own Resource Management, so it's imune to KSP 1.9 "bruteness".
* Firespitter
	+ I detected no problems on it
* Anything that supports (or it's supported by) TweakScale
	+ TweakScale already handles KSP-Recall, so anything that makes use of `Scale_Redist` is already covered
	+ Modular Fuel Tanks is one of that Add'Ons, as long nobody shoves its on parts that already have another Fuel Switch.
* B9 Parts Switch should work. As longe there're no other Fuel Switch installed on the same part.
* Other fuel switches:
	+ As long the OnEditorVesselModified is fired on each change (or one of the Custom Events from Recall, see below), it should work fine.
	+ Please report on this thread anything that you thing is wrong. I'm fixing them as I'm aware of them.

### Crafts at rest changes drifts the Heading by their own

On TweakScale's [Forum thread](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24321-2020-00804/&do=findComment&comment=3836931), a discussion about a problem introduced on KSP 1.8 that made the crafts to drift the heading randomly when leaved at their own, even with parking breaks or not wheels at all!

KSP-Recall introduces a new work around for this problem, detecting when the Heading drift is unwanted and canceling it.

It **does not** fixes, yet, the drift induced by the wheels itself (that were being summed up with this one).


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
			- You relicense (and fully comply) your works using GPL 2.0 (or later)
			- You don't mix your work with GPL incompatible works.
	* If by some reason the GPL would be invalid for you, rest assured that you still retain the right to Use the Work under SKL 1.0. 

Please note the copyrights and trademarks in [NOTICE](./NOTICE).


## UPSTREAM

There's no upstream, **I am (g)ROOT** :)
