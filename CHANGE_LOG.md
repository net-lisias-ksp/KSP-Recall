# KSP-Recall :: Change Log

* 2020-0817: 0.0.4.2 (LisiasT) for KSP >= 1.8.0 PRE-RELEASE
	+ Fixes an annoying situation where Decouplers and Docking Ports with `Driftless` blocks fuel to engines above them on the stack. 
* 2020-0817: 0.0.4.1 (LisiasT) for KSP >= 1.8.0
	+ An error on handling parts without `RigidBody` was raining NREs on the KSP.log. Fixed.
	+ A slightly smarter handling of inactive and rigidbodyless parts may save a tiny little bit of CPU time.
	+ Specialised treatment for Kerbals on EVA, as it was realised that Kerbals drifts a lot more than crafts - by reasons still unknown at this moment.
* 2020-0815: 0.0.4.0 (LisiasT) for KSP >= 1.8.0
	+ Adds a Work Around for crafts drifting on the Heading at rest, even when without wheels attached.
		- There's another similar problem on the wheels themselves, KSP Recall are still working on this one
	+ **Way** smarter selective applying of the Modules when needed.  
* 2020-0714: 0.0.3.2 (LisiasT) for [1.9.0 <= KSP <= 1.9.1]
	+ Maintenance Release.
		- Better (and safer) deactivation code using info gathered from [TweakScale](https://github.com/net-lisias-ksp/TweakScale/issues/125).
		- Locking up the current features to work only on 1.9.x (as KSP 1.10 doesn't need them).
* 2020-0521: 0.0.3.1 (LisiasT) for [1.9.0 <= KSP <= 1.9.1]
	* Fixes the 1.9.x Editor glitch on cloning parts with Resources' amount changed.
* 2020-0518: 0.0.3.0 (LisiasT) for [1.9.0 <= KSP <= 1.9.1]
	* Adds Sanity Check to prevent misuse.
	* New Event `OnPartResourcesChanged`.
		* `OnPartResourceChanged` is deprecated, but still works.
		* Note the **S** on resources.
* 2020-0305: 0.0.2.3 (LisiasT) for [1.9.0 <= KSP <= 1.9.1] PRE-RELEASE
	+ Fixes Issue [#5](https://github.com/net-lisias-ksp/KSP-Recall/issues/5).
		- Thanks, [Vegetal](https://forum.kerbalspaceprogram.com/?app=core&module=members&controller=profile&id=147251), for the [heads up](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-ksp-recall-0022-pre-release-2020-0304/&do=findComment&comment=3752047)! 
* 2020-0304: 0.0.2.2 (LisiasT) for [1.9.0 <= KSP <= 1.9.1] PRE-RELEASE
	+ Allowing Fuel Switches that eliminates all the Resources from a part to be protected
	+ Embedding KSPe.Light (avoiding an external dependency).
* 2020-0303: 0.0.2.1 (LisiasT) for [1.9.0 <= KSP <= 1.9.1] PRE-RELEASE
	+ ** DROPPED ** 
* 2020-0302: 0.0.2.0 (LisiasT) for KSP >= 1.4.1 PRE-RELEASE
	+ A not so initial Release for Evaluation, but really made right this time. Honest!
	+ Correctly (I think) implements the Issue [#1](https://github.com/net-lisias-ksp/KSP-Recall/issues/3) "KSP 1.9.x resets resources to prefab while cloning parts".
		- Listening to the new Event `OnPartResourceChanged`
	+ Fixes the Issue [#2](https://github.com/net-lisias-ksp/KSP-Recall/issues/2) "Resourceful is applying the last scaling of a part on every new part"
* 2020-0301: 0.0.1.1 (LisiasT) for KSP >= 1.4.1 PRE-RELEASE
	+ Initial Release for Evaluation made right this time.
* 2020-0301: 0.0.1.0 (LisiasT) for KSP >= 1.4.1 PRE-RELEASE
	+ Initial Release for Evaluation
	+ Currently compatible only with TweakScale
