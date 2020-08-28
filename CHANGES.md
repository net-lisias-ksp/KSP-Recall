# KSP-Recall :: Changes

* 2020-0827: 0.0.4.4 (LisiasT) for KSP >= 1.8.0
	+ Github, with all their wisdom, decided to "help me" adding silently a configuration that automatically converts EoL between UNIX and Windows. And did the stunt on a DLL.
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
