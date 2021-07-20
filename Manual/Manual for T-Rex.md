# Manual for T-Rex 0.2.0

[TOC]

## Introduction

### Info

<img src="Img\TRexLogoOk.png" alt="TRexLogoOk" style="zoom:67%;" />

Hello! Thank you for using T-Rex! ;)

T-Rex is an open-source plug-in for Grasshopper.

**Purpose: **T-Rex can help you to create parametric models of reinforced concrete structures with Grasshopper.

**Requirements:** Rhino 6 or Rhino 7

**Status:** BETA, Work in progress

**Contact:** If you have any specific questions, email me: w.radaczynski@gmail.com

**Website:** www.code-structures.com

**Videos:** You can find videos on my YT channel here: https://www.youtube.com/channel/UCfXkMo1rOMhKGBoNwd7JPsw

**Used libraries:**

- xbim: for IFC export: https://docs.xbim.net/, license: https://docs.xbim.net/license/license.html

### License (MIT License)

Copyright © 2021 Wojciech Radaczyński

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to
do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

### How does T-Rex work

T-Rex will help you to model thousands of the rebars with Grasshopper. The secret of it's speed is in the discrete mesh representation of the rebars.

Basically if you want to model a pipe, that will represent a single bar, using only built-in components: you'll probably choose "Pipe" component. Pipe component is really precise (as all of the NURBS-based components), the baked model of the pipe would be lightweight, but the problem is: it's also will be pretty slow. It will be visible if you decide to create, let's say, thousands of them. The more complex the shape of the bar is, the more time it will take to create a preview.

T-Rex will create a discrete mesh model of the pipe, which is **less precise**, but it will render **much quicker**, which is a huge advantage when we talk about parametric modeling, that should be executed just-in-time.

<img src="Img\MeshNurbs.png" alt="MeshNurbs" style="zoom:80%;" />

You can see the difference in precision on the image above. Let's look at the speed:![MeshSpeed](Img\MeshSpeed.png)

Meshes even for 10000 rebars works pretty fast. Making the same with the pipes makes slider freeze for some time, depending on the shape of the pipe.

**Important note:** Discrete models are problematic when you will try to measure things on your own. For example, if you decide to measure the volume by taking a mesh, then you'll get wrong results:

<img src="Img\MeshVsNurbsVolume.png" alt="MeshNurbsVolume" style="zoom:67%;" />

Volume of rebar with diameter: 8 and length: 100 should be around 5026.5482. As you can see Pipe component will give you a precise model and the result is close enough, but the discrete mesh result is far from the truth. That's why you shouldn't measure the mesh results. You should use proper components that calculate it by themselves:

![ComponentVolume](Img\ComponentVolume.png)

For this reason: remember, that if you're using Mesh To Elements component - the volume and mass calculated will not be precise, because of this type of representation.

### Units

Grasshopper is unit-less, you can read more about it there: https://www.grasshopper3d.com/forum/topics/units-m-ft

So is the T-Rex. It means that you will have to choose your unit system by yourself. If you choose to use meters then make sure that every dimension is in meters and the Rhino document related to Grasshopper script is set to meters.

**Important note:** In T-Rex 0.2.0 IFC export is always treating the model as it is in **millimeters**.

### Tolerances

There are two places where you have to set proper tolerances values:

- Rhino document

  To understand better Rhino's tolerances - read this article: https://wiki.mcneel.com/rhino/faqtolerances

  **Important note:** Grasshopper also takes tolerances settings from Rhino document's settings. You can read about it there: https://www.grasshopper3d.com/forum/topics/gh-tolerance?commentId=2985220%3AComment%3A908767

- Inside the T-Rex components

  There are a few components that require Tolerance value as input. Most of the time default values will be sufficient. To understand it more: you will have to see the source code.

## Concrete

### About

Concrete components will help you to create typical shapes of concrete elements.

In fact you will model a group of elements at once. This way it is easier to create smaller IFC files, because the reference to the same property (like materials, profiles, etc.) will be putted once for whole group.

### Components

#### Profile

![Profile](Img\Profile.png)

All points that defines profile should be placed on XY Plane.

Profile can be used to create elements with Profile To Elements.

#### Profile To Elements

When we have an element, that can be represented as Sweep along the curve: we can use this component. At first we need to create the profile with Profile component. Then we can use this profile to sweep it along given lines:

![ProfileToElement](Img\ProfileToElement.png)

It's not easy to obtain needed position of the profile along the line. That's why there is Rotation Angle as input, which allows to rotate the profiles of elements.

Profile Elements look empty inside, but this just the visual representation, they are treated as if they were closed solids - you can see it after IFC export.

#### Mesh To Elements

Mesh elements should be used when the geometry of an element is complex and it cannot be represented in any different way. Meshes have to store a lot of information (about vertices coordinates, faces), which will affect the size of the IFC file.

Only closed meshes are allowed as input.

![Dino](Img\Dino.png)

![Stairs](Img\Stairs.png)

If the same element should be placed in different places, then put use multiple Insert Planes as input:

![Tube](Img\Tube.png)

**Important note:** remember, that mesh representation is not always precise, so the Volume and Mass calculated would not precise as well, because of the discretization.

#### Element Group Info

This component allows to read the properties of the group of elements.

Remember that Volume and Mass is a sum of the whole group.

## IFC

### About

Tools for IFC files creation.

T-Rex is using xbim library for IFC export.

T-Rex 0.2.0 allows only models in **millimeters** to be exported to IFC.

### Components

#### Create IFC

Component creates an .ifc file of given groups of elements. Schema supported: **IFC4x1**.

I test IFC export on 2 free IFC viewers:

- BIMvision (https://bimvision.eu/)
- Open IFC Viewer (https://openifcviewer.com/)

You have to understand one thing: IFC is so complex, that depending on a software you're using for import - the results might differ. Some software can miss some types of geometries or information. In my opinion it's because of the complexity of the IFC schema, which makes it hard for each software developer to support all these features, types of geometries and so on.

![IFC](Img\IFC.png)

To Groups input you can plug Rebar Groups and Element Groups.

Path should have .ifc extension at the end.

## Properties

### About

Properties components will help you to put the informations about different properties, that will be used later to create different objects.

### Components

#### Bending Roller

Not all of the Rebar Shape components, but many of them, require Bending Roller as input. This values are required to make fillets of the polyline correctly. Look at the image below to see how it works:

![BendingRoller](Img\BendingRoller.png)

60 is just an example: the proper value is for you to decide, it is often described in different standards / documents, and it can depend on things like for example diameter of the rebar or other factors.

To understand Tolerance and Angle Tolerance check Tolerances chapter. For most of the cases default value will be sufficient. Basically those values will be used to create proper fillets of given polyline.

**Important note:** If the value of Bending Roller Diameter will be too large, then in some cases Rebar Shape can be created wrong. Check the dimensions of the Rebar Shape's geometry to make sure if it's correct.

#### Cover Dimensions

This component will create an offset for Rebar Shapes components that require Rectangle as an input.

![CoverDimensions](Img\CoverDimensions.png)

To see how it works we have to look on a Rebar Shape result. If you plug 0 for all of the inputs, you will get this:

![CoverDimensions0](Img\CoverDimensions0.png)

So if you want to offset this stirrup / bar in the boundary rectangle, you will have to change a values:

![CoverDimension1](Img\CoverDimension1.png)

It works similar for all of the Rebar Shape components that require Rectangle input.

#### Material

Material will help you to add some informations about material of the objects.

![Material](Img\Material.png)

The important thing is density, as it will affect the result of calculating the weight of the objects.

**Check the Units chapter to find more informations.**

Basically, if you want to have a density 7850 kg/m^3 by plugging slider with the value 7850, then you have to make sure that all of the other dimensions are in meters. Then you will get the result of weight in kilograms.

If your whole model is in millimeters, and you still want to get the weight in kilograms, then you have to plug in the density in kg/mm^3.

#### Rebar Properties

This component will take Diameter and Material as an input.

![RebarProperties](Img\RebarProperties.png)

Then you can plug those properties to create Rebar Shapes objects.

## Rebar Shape

### About

Rebar Shape components will help you to create different shapes of the rebars. Those objects can be later used to create objects called Rebar Group.

Every component from that section will output Rebar Shape and Mesh. Mesh is a discrete model of the rebar (you can read more about it in the Introduction chapter). Rebar Shape will be useful to create Rebar Group.

### Components

#### Curve To Rebar Shape

This component converts curve to a Rebar Shape. You can basically draw any curve you'd like and convert it to the rebar.

![CurveToRebarShape](Img\CurveToRebarShape.png)

<img src="Img\CurveToRebarExample.png" alt="CurveToRebarExample" style="zoom:50%;" />

#### Polyline To Rebar Shape

This component converts polyline to Rebar Shape. It requires one more output then Curve To Rebar Shape: Bending Roller. So at first this polyline will be filleted, and then it will be translated to Rebar Shape.

![PolylineToRebarShape](Img\PolylineToRebarShape.png)

There are few requirements: polyline can't be straight and there need to be at least 3 points of polyline - those requirements are to make sure that it is possible to create fillets. If you want to draw a straight line, and turn it to Rebar Shape - use Curve To Rebar Shape component instead.

#### L-Bar Shape

![L-BarShape](Img\L-BarShape.png)

#### Spacer Shape

![SpacerShape](Img\SpacerShape.png)

<img src="Img\SpacerShape3d.png" alt="SpacerShape3d" style="zoom:67%;" />

#### Stirrup Shape

![StirrupShape](Img\StirrupShape.png)

Stirrup Shape requires additional info about the type of the hooks. Right now there are 2 types:

- Type = 0:

  ![StirrupShapeHook0](Img\StirrupShapeHook0.png)

- Type = 1:

  ![StirrupShapeHook1](Img\StirrupShapeHook1.png)

#### Rectangle To Line Bar Shape

![RectangleToLine](Img\RectangleToLine.png)

You can also change the position from 0 to 3, for example let's try with type 1:

![RectangleToLinePosition1](Img\RectangleToLinePosition1.png)

#### Rectangle To Stirrup Shape

![RectangleToStirrup](Img\RectangleToStirrup.png)

Check Stirrup Shape to see how different hooks for stirrups work.

#### Rectangle To U-Bar Shape

![RectangleToUBar](Img\RectangleToUBar.png)

Position input works similar to the Rectangle To Line Bar Shape, for example if you set type 1:

![RectangleToUBarPosition1](Img\RectangleToUBarPosition1.png)

## Rebar Spacing

### About

These components are useful to create objects called Rebar Groups. They will take your Rebar Shape and generate group of rebars with proper spacing.

There is one exception: Custom Spacing component that doesn't create any spacing, you have to plug Rebar Shapes that have already spaces between them there.

#### Id

The Rebar Spacing components require Id which is an integer number. That Id is only for you, to help you organize those groups.

### Components

#### Curve Spacing

This component is useful to create spacing that has a complex geometry. It allows the user to draw a curve and use it as an input of the spacing geometry. That curve will be divided into planes that are perpendicular to the curve, so the rebars will be oriented in those new division planes.

![CurveSpacing](Img\CurveSpacing.png)

![CurveSpacingCircle](Img\CurveSpacingCircle.png)

The issue is that sometimes those new division planes are rotated in the different way that we want. That's why there is Rotation Angle input - so you can rotate it as you want.

If you right click this input you can click Degrees, so there is no need to play with radians.

Shape's Origin Plane = World XY Plane. Basically you should place the Rebar Shape inside XY Plane, because it will be copied from this plane to all of the division planes that were created along the curve.

![DivisionPlanes](Img\DivisionPlanes.png)

#### Custom Spacing

This component will help you to create Rebar Group with custom spacing, which means you will have to create spacing between Rebar Shapes by yourself, and then connect all of those Rebar Shapes to the component.

![CustomSpacing](Img\CustomSpacing.png)

Basically it takes all of the Rebar Shapes you connect and add it to the one Rebar Group.

For the example above - Vector Count Spacing or Vector Length Spacing components will be better. Custom Spacing will be helpful where you want for example to create one group for rebars with different lengths.

![TriangleSpacing](Img\TriangleSpacing.png)

It can be also only one Rebar Shape without any spacing.

![Custom1Shape](Img\Custom1Shape.png)

#### Vector Count Spacing

This component takes Rebar Shape and treat it as the first rebar of the group (start of the spacing). Vector defines where the end of the spacing will appear. Count defines how many rebars will be in this group. Spacing between all of those bars will be the same from start to the end of the input Vector.

![VectorCountSpacing](Img\VectorCountSpacing.png)

#### Vector Length Spacing

This component is similar to Vector Count Spacing, the difference is: you don't define Count (how many rebars you need in the group) but you have to define Spacing Length instead. There are different types of spacing so let's go through all of them:

- Type: 0 - Constant spacing with different last one

  ![VLS0](Img\VLS0.png)

- Type: 1 - Constant spacing with different first one

  ![VLS1](Img\VLS1.png)

- Type: 2 - Constant spacing with first and last different

  ![VLS2](Img\VLS2.png)

- Type: 3 - Smaller (or the same) spacing length, but constant for all bars from start to end

  ![VLS3](Img\VLS3.png)

Tolerance default value should be sufficient for most of the cases, to understand it more you'll need to check the source code.

**Important note:** For some combination of Vector and Spacing Length you'll get the result in which rebars are in collision with each others, for example:

![Collision](Img\Collision.png)

Most of the time it will be visible, but sometimes the distance between collided bars is so small, that you cannot barely see the difference if one rebar is inside an another or is it just one bar. That's why you should always make sure that the Rebar Group is created correctly.

#### Rebar Group Info

This component show all of the informations about created Rebar Group.

<img src="Img\RebarGroupInfo.png" alt="RebarGroupInfo" style="zoom:67%;" />

All of those information are unit-less - check Unit chapter to read more about it.

**Important note:** Grasshopper often shows values in the scientific notation. The calculated value can be more precise in reality. To see the value in a format you want, you can use Grasshopper component called Format. You can read more about it here: https://www.grasshopper3d.com/forum/topics/formatting-numbers-in-grasshopper

<img src="Img\Format.png" alt="Format" style="zoom:67%;" />

 