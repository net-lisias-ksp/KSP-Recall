# KSP-Recall :: Changes

* 2022-0219: 0.2.2.1 (LisiasT) for KSP >= 1.4.1
	+ Formally closes issues:
		- [#35](https://github.com/net-lisias-ksp/KSP-Recall/issues/34) AttachedOnEditor is not working for SubAssemblies.
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
