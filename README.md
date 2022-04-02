

# Vaporwave reality racer

## Setting up for development
The unity project uses version 2022.1.0b9 so make sure you download the proper unity installation.

All unity versions can be found here:
https://unity3d.com/get-unity/download/archive

Download and use unity hub to manage all of your unity installations/projects:
https://unity3d.com/get-unity/download

In unity hub, add an existing project by clicking add and navigate to where you downloaded this repository. Make sure you're in the root directory where you can see the 'Assets' folder when you click open.

## PORTALS

The portals are implemented in vfx graphs found in Assets/VFX
currently there is only portal1 but more to come.
Portals can be easily manipulated through different variables available in the graph.
My favorite one to mess with is the color over lifetime seen here

![alt text](https://github.com/logandauria/Vaporwave-Reality-Racer/blob/master/images/portal_colorlifetime.png?raw=true)

An implementation of the portal can be seen in the scene "Game" in Assets/Scenes


## Developments

### Creating a particle mesh
Unity has a useful tool called the point cache bake tool (Window>Visual Effects>Utilities>Point Cache Bake Tool) where you input a mesh and it exports a set of points that can be used by VFX graphs.
This was used to create a point graph of a person which was imported into a custom VFX graph.
Here's a tutorial explaining most of the process: https://www.youtube.com/watch?v=j1R1Uelroco&t=101s

Note: Point cache bake tool works best with meshes from .obj files. Problems were found using it with .fbx files.

### Editing the VFX forces

![alt text](https://github.com/TeamSally/SallyProject/blob/master/images/vfxtest2.png)

**Bias** - the value threshold for when a beat will occur to music. Higher values are for stronger notes. Range is 0-100  
**Time Step** - the amount of time required between two beats  
**Time to Beat** - the amount of time it takes for an effect to trigger  
**Rest Smooth Time** - the amount of time it takes to revert back to the original state defined by the restVector  
For the beat and rest vector, the xyz values correlate to the following: (X: intensity, Y: drag, Z: frequency)  
Intensity is the strength of the force  
Drag is the directional force (i think lol)  
Frequency seems to be a preset movement pattern based on the provided value/seed  
**Beat Vector** - The values that will be set once a beat occurs  
**Rest Vector** - The default values that the graph goes back to when there are no beats occuring  
**Random Range** - Will activate functionality for the XYZ values to be randomly selected between a range of the beat vector and rest vector  
  
  
![alt text](https://github.com/TeamSally/SallyProject/blob/master/images/vfxsphereconform.png)  
  
**Attraction Speed** - How fast the particles conform to the sphere  
**Attraction Force** - How strong the conforming force is  
**Stick Distance** - How far away the particles will tend to stick  
**Stick Force** - How strong the sticking force  


![alt text](https://github.com/TeamSally/SallyProject/blob/master/images/vfxvelocitycurves.png)

These velocity curves apply different forces based on time passed. You can add infinite points on the graph
  
For the orb, there are 2 VFX graphs used so you can change all of the above values for both. They don't have to be the same and will probably look cooler with different values


### Creating new point caches from meshes

Import an obj into the assets folder of the project, or select a preexisting one with the point cache bake tool  
Here's how to open the point cache bake tool:

![alt text](https://github.com/TeamSally/SallyProject/blob/master/images/pointcache.png?raw=true)


