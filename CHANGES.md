# KSP-Recall :: Changes

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
