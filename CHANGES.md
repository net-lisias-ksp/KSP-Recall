# KSP-Recall :: Changes

* 2022-1122: 0.3.0.4 (LisiasT) for KSP >= 1.4.1
	+ Adds a check for the `pwd` problem that started to happen when people tried to get rid of the PD-Launcher in an unfortunate way. 
		- Check [this](https://forum.kerbalspaceprogram.com/index.php?/topic/210419-get-rid-of-the-stupid-launcher-nobody-likes-them-and-they-do-nothing-but-ruin-the-gaming-experience/) for the whole history.
		- Additional links (**do not** try them, they cause this problem!)
			- [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/210409-how-to-bypass-the-new-launcher-added-in-update-1124-steam/) 
			- [Reddit](https://www.reddit.com/r/KerbalSpaceProgram/comments/ym04gx/solution_for_broken_mods_caused_by_the_launcher/)
	+ Updates to the latest KSPe.Light.
* 2022-0908: 0.3.0.3 (LisiasT) for KSP >= 1.4.1
	+ Updating the Sanity Check to prevent an annoying "Proceed with caution" warning.
* 2022-0904: 0.3.0.2 (LisiasT) for KSP >= 1.4.1
	+ After some weeks of denying, it was **finally** realised that `AttachedOnEditor` is needed downto KSP 1.4.3 - on the very first `ModulePartVariant` implementation.
		- What a crap of a code they published, damn!
 	+ Fixes a small brain fart of mine on the [INSTALL.md](https://github.com/net-lisias-ksp/KSP-Recall/blob/master/INSTALL.md) file.
	+ Updates KSPe.Light to the latest release (2.4.2.1 at this time)
	+ Closes issues:
		- [#55](https://github.com/net-lisias-ksp/KSP-Recall/issues/55) The KSP Editor is screwing things since 1.4.3
		- [#50](https://github.com/net-lisias-ksp/KSP-Recall/issues/50) Not sure how to install 0.3.0.0
			- Thanks to [@tomtheisen](https://github.com/tomtheisen) for the heads up!
* 2022-0630: 0.3.0.1 (LisiasT) for KSP >= 1.4.1
	+ Fixes a **huge** brain fart of mine from 0.2.2.4 #facePalm
	+ Closes issues:
		- [#52](https://github.com/net-lisias-ksp/KSP-Recall/issues/52) REPORT: Diagnosing the GoAHead Issue
		- [#49](https://github.com/net-lisias-ksp/KSP-Recall/issues/49) ArgumentOutOfRangeException causes saving to break and makes my save file unplayable
* 2022-0626: 0.3.0.0 (LisiasT) for KSP >= 1.4.1
	+ Prevents a race condition with Making History on Main Menu on heavily loaded rigs. 
	+ Updates ModuleManagerWatchDog to 1.1.0.1
	+ Updates KSPe.Light.Recall to 2.4.1.16 due the [Proton fix](https://github.com/net-lisias-ksp/KSPe/issues/31).
	+ Closes issues:
		- [#45](https://github.com/net-lisias-ksp/KSP-Recall/issues/45) Move the Sanity Checks) out of the Main Menu startup
* 2022-0514: 0.2.2.4 (LisiasT) for KSP >= 1.4.1
	+ Closes issues:
		- [#41](https://github.com/net-lisias-ksp/KSP-Recall/issues/41) Investigate a possible (bad) iteraction with Procedural Parts (RO) V2.3.0
		- [#40](https://github.com/net-lisias-ksp/KSP-Recall/issues/40) Unhappy interaction with Deep Freeze?
		- [#37](https://github.com/net-lisias-ksp/KSP-Recall/issues/37) Check about a missing use-case on AttachedOnEditor
		- [#21](https://github.com/net-lisias-ksp/KSP-Recall/issues/21) Update KSPe.Light for KSPe
