# KSP-Recall :: Known Issues

* A new problem was found, affecting Editor since KSP 1.9, where some parts are triggering a strange misbehaviour when you Alt+Click a part for a duplicate, when Loading a craft for Merge or when loading a SubAssembly
	+ It was determined that when Parts **without** `ModulePartVariant` is the root of the SubAssembly, the parts attached to it **must have** the `ModulePartVariant` otherwise the problem will be triggered
	+ I realised that the problem is that the Positions on the Attachment Nodes are not being initialised by the vanilla Parts - apparently the initialisation was moved to the `ModulePartVariant` module.
		- This may be the reason Squad choose to shove prefab back into the craft when loading it from the Editor, as apparently they didn't were able to pinpoint the cause of the misbehaviour...
	+ Problem: I solved the problem for SubAssemblies and Craft files saved **after** installing the newest Release of KSP-Recall, but didn't managed to cook a way to salvage the pre-existent ones.	 
	+ For more information:
		- [Got a bug with a subassembly here](https://forum.kerbalspaceprogram.com/index.php?/topic/206784-got-a-bug-with-a-subassembly-here/#comment-4090098) on Forum
		- [Issue \#34 on GitHub](https://github.com/net-lisias-ksp/KSP-Recall/issues/34#issuecomment-1034483251)
* KSP 1.11 also introduced another bug, this one about a miscalculation on recovering costs when a craft is recovered.
	+ A new Module `Refunding` and a *Meta Resource* called `RefundingForKSP111x` was created to handle this.
	+ Not all Stackable parts are being correctly refunded at this moment. 
* KSP 1.11 introduced a new bug on launching vessels using some old parts (as OPT and Firespitter). The problem appears to be on initialising the heat of such parts.
	+ Check the following posts for more information:
		- [KAX](https://forum.kerbalspaceprogram.com/index.php?/topic/180268-131/page/9/&tab=comments#comment-3901075)
		- [Impossible Innovations](https://forum.kerbalspaceprogram.com/index.php?/topic/175694-131/&do=findComment&comment=3901072).  
*  Wheels and Landing Legs **also** drifts, but due different reasons - so, in fact, we have **two** sources of drifting on KSP >= 1.8 right now. But the problem `Driftless` solves helps, and helps a lot, on preventing the situation where the Legs and Wheels start to be a problem!
	+ So, yeah... I was wrong, this thing is more a fix than a workaround by this time! :)  
* I was wrong too on adjusting the *Maximum Physics Delta-Time Per Frame* to 0.03. It make things worse on CPUs under pressure!
	+ So, if you have CPU to spare (i.e., never gets the yellow time on the top left of the screen), raising MPDTPF can help.
	+ If your CPU is near the limit, it will make things worse!
* The part count on the current scene **is the real key**
	+ Big crafts tend to drift on loaded CPUs because somehow the MPDTPF appears to be abbreviating the FixedUpdate processing, and no not all parts are zeroed.
		- If I'm right, high count crafts ended up with only some of the parts fixed, and then the driftness induced by the others prevails
		- And the part count of the whole **scene** (i.e., everything that is unpacked and it's on the current Physics Range) is what counts, no the part count of the current craft!
	+ Kerbals are just another part.
		- So they will drift or not depending in where on the list of parts for the current Scene they are! 
* KSP-Recall *WORKS FINE* with Module Manager.
	+ [Anyone telling you](http://ksp.lisias.net/add-ons/ModuleManager/WatchDog/Screen%20Shot%202020-07-14%20at%2002.14.51.png) you need an alternative fork of MM is telling you nonsense.

- - -

* RiP : Research in Progress
* WiP : Work in Progress
