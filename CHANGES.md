# KSP-Recall :: Changes

* 2023-0325: 0.4.0.0 (LisiasT) for KSP >= 1.4.1
	+ Reworks `Refunding` (yet again), splitting the `PartModule` into two, the second one (`StealBackMyFunds`) dedicated to the Funds on `float` problem.
	+ Reworks the `AttachedOnEditor`, fixing an annoying "gap" when merging crafts (that didn't happens on SubAssemblies!)
	+ Updates Module Manager Watch Dog to the latest.
	+ Closes issues:
		- [#62](https://github.com/net-lisias-ksp/KSP-Recall/issues/62) Find a way to survive KSPCF's ~~Stupidity~~ *Less Than Smartness*
		- [#61](https://github.com/net-lisias-ksp/KSP-Recall/issues/61) AttachedOnEditor is being screwed up when Merging crafts
		- [#28](https://github.com/net-lisias-ksp/KSP-Recall/issues/28) Refresh the ModuleManagerWatchDog DLL
