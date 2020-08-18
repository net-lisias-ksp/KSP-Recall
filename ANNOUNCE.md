## ANNOUNCE

[KSP Recall 0.0.4.1](https://github.com/net-lisias-ksp/KSP-Recall/releases) in on the Wild, featuring:

* A fix for the infinite NRE on the *dedriftification* - some parts just don't have a `RigidBody`, and I didn't told the code about it.
* Kerbals on EVA suffers 20 times more drifting than the craft used on the testing(Aegis 3A). They now drifts less too.
* Saving a tiny bit of CPU doing smarter handling of inactive and *rigidbodyless* parts.

On my tests, I managed to leave a craft alone for 15 minutes without drifting a single degree, but once I got about 22 degrees after 45 minutes (still better than 1 degree each 2 or 3 seconds, though)- as I said: it's a work around, not a fix.

Female Kerbals **apparently** drift more than males - the *spurious velocity* on males never reaches 18mm/s, but the females reached about 20 to 21mm/s. At least, on my rig. This information may be biased however, apparently VesselMover is spawning preferably females on my savegame, and so perhaps the extra drift is due this Kerbal being spawned second.

For sadistic Kerbonauts that don't refrain themselves from torturing their gaming machines, adjusting the *Maximum Physics Delta-Time Per Frame* to 0.03 gave some marginal positive results on the crafts on my i7 MacPotato, but the Kerbals drifted sensibly more. Given the randomness of the amount of drifting we get on every game restart, take this information with a huge grain of salt.

Note: Wheels and Landing Legs **also** drifts, but due different reasons - so, in fact, we have **two** sources of drifting on KSP >= 1.8 right now. The present Recall version does not (yet) fixes the wheels' drift (besides helping a very little bit, as there's one less source of drifting now).

Finally, the `Driftless` PAW now appears only on flight. `Resourceful` still appears only on Editor.

Good Luck!  

- - - 

This Release will be published using the following Schedule:

* GitHub, reaching manual installers first and users of KSP-AVC. Right now.
* CurseForge, Right now.
* SpaceDock (and CKAN users), Right now.
