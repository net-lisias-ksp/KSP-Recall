# KSP-Recall :: Changes

* 2021-0718: 0.2.0.2 (LisiasT) for KSP >= 1.4.1
	+ `ChillingOut` is still needed on 1.12.x series. Reactivating it.
	+ Some missing logging stats added.
* 2021-0627: 0.2.0.1 (LisiasT) for KSP >= 1.4.1
	+ Compatibility to KSP 1.12.0 is confirmed.
		- `Refunding` is still needed, sadly...
	+ Closes issues:
		- [#23](https://github.com/net-lisias-ksp/KSP-Recall/issues/23) Refunding is triggering a nasty memory leak on this.part.Modules.Add
		- [#22](https://github.com/net-lisias-ksp/KSP-Recall/issues/22) Allow Refunding to be used on KSP 1.12.0
		- [#21](https://github.com/net-lisias-ksp/KSP-Recall/issues/21) Unity's spinlocks are bullying the Garbage Collector, and Refunding is not helping on the situation.
* 2021-0508: 0.2.0.0 (LisiasT) for KSP >= 1.4.1
	+ Bumping version due a [mishap](https://github.com/net-lisias-ksp/KSP-Recall/issues/17) on the latest release on SpaceDock
	+ Trying to workaround [MAS being picky](https://github.com/net-lisias-ksp/KSP-Recall/issues/18) even on hidden resources.
