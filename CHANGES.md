# KSP-Recall :: Changes

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
* 2022-0308: 0.2.2.3 (LisiasT) for KSP >= 1.4.1
	+ Allows patching `AttachedOnEditor` on every compatible part, no matter it has `TweakScale` installed or not.
		- Needed because a TweakScaled part borks when attached to another without it. 
* 2022-0228: 0.2.2.2 (LisiasT) for KSP >= 1.4.1
	+ Reworks:
		- [#35](https://github.com/net-lisias-ksp/KSP-Recall/issues/35) AttachedOnEditor is not working for SubAssemblies.
* 2022-0219: 0.2.2.1 (LisiasT) for KSP >= 1.4.1
	+ Formally closes issues:
		- [#35](https://github.com/net-lisias-ksp/KSP-Recall/issues/35) AttachedOnEditor is not working for SubAssemblies.
		- [#34](https://github.com/net-lisias-ksp/KSP-Recall/issues/34) **NEW** Misbehaviour on KSP introduced by AttachedOnEditor
* 2022-0214: 0.2.2.0 (LisiasT) for KSP >= 1.4.1 PRE-RELEASE
	+ Implements another missing use case, handled on #34
	+ Works the issues:
		- [#34](https://github.com/net-lisias-ksp/KSP-Recall/issues/34) **NEW** Misbehaviour on KSP introduced by AttachedOnEditor
* 2022-0211: 0.2.1.4 (LisiasT) for KSP >= 1.4.1
	+ Implements a missing use case, handled on #34
	+ Works the issues:
		- [#34](https://github.com/net-lisias-ksp/KSP-Recall/issues/34) **NEW** Misbehaviour on KSP introduced by AttachedOnEditor
* 2021-1221: 0.2.1.3 (LisiasT) for KSP >= 1.4.1
	+ Implements a missing Use Case from Issue #32, handled on #34
	+ Lifts the ban for KSP >= 1.10, the thing is known to work on everything since KSP 1.9
	+ Works the issues:
		- [#34](https://github.com/net-lisias-ksp/KSP-Recall/issues/34) **NEW** Misbehaviour on KSP introduced by AttachedOnEditor
* 2021-1221: 0.2.1.2 (LisiasT) for KSP >= 1.4.1
	+ Rework of issue #32.
	+ Closes issues:
		- [#32](https://github.com/net-lisias-ksp/KSP-Recall/issues/32) **Correctly** handle KSP 1.9 (and later) borking while loading Crafts on Editor with scaled Variants
* 2021-1221: 0.2.1.1 (LisiasT) for KSP >= 1.4.1
	+ Locks up the current `AttachedOnEditor` as it is working **fine** on KSP 1.9.x
		- KSP 1.10.0 and newer, however, apparently need a different workaround, currently WiP. 
* 2021-1220: 0.2.1.0 (LisiasT) for KSP >= 1.4.1
	+ **Finally** tackles down the Editor's Surface Attachment problem introduced by KSP 1.9.0.
		- By default, locked to Parts with TweakScale only - but the patch can be easily extended if needed. 
	+ Closes issues:
		- [#32](https://github.com/net-lisias-ksp/KSP-Recall/issues/32) **Correctly** handle KSP 1.9 (and later) borking while loading Crafts on Editor with scaled Variants
