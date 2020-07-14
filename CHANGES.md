# KSP-Recall :: Changes

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
