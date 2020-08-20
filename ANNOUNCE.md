## ANNOUNCE

[KSP Recall 0.0.4.3](https://github.com/net-lisias-ksp/KSP-Recall/releases) in on the Wild, featuring:

* A fix for the infinite NRE on the *dedriftification* - some parts just don't have a `RigidBody`, and I didn't told the code about it.
	+ And now without blocking fuel when applied on Decouplers and Docking Ports below engines on staging. 
* Saving a tiny bit of CPU doing smarter handling of inactive and *rigidbodyless* parts.

On my tests, I managed to leave a craft alone for 15 minutes without drifting a single degree, but once I got about 22 degrees after 45 minutes (still better than 1 degree each 2 or 3 seconds, though) - it all depends of your CPU and how many parts are being active on the scene.

Kerbals are just another part on the scene. Depending on how many parts are in physics range (and their position on the list), they will or will not drift even with Recall installed.

For sadistic Kerbonauts that don't refrain themselves from torturing their gaming machines, adjusting the *Maximum Physics Delta-Time Per Frame* may render good results. If your CPU is near the limit (flickering the timestamp on top left to yellow), you may want to increase it a bit.

Constant CPU overload (time stamp on top left is always yellow or even red) are currently known to heavily inducing drifting, as this situation apparently aborts the FixedUpdate on some Modules (and so `Driftless` is rendered useless).

Note: Wheels and Landing Legs **also** drifts, but due different reasons. However, it was realised that `Driftless` prevents, most of the time, the situation that triggers the Wheels and Legs drift. So it ended up being a fix, after all. :)

Finally, the `Driftless` PAW now appears only on flight. `Resourceful` still appears only on Editor.

Good Luck!  

- - - 

This Release will be published using the following Schedule:

* GitHub, reaching manual installers first and users of KSP-AVC. Right now.
* CurseForge, Right now.
* SpaceDock (and CKAN users), Right now.
