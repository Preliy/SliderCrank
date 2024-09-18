# Slider Crank
Unity package for simplified representation of slider crank mechanism.

![SliderCrank.gif](Documentation%2FSliderCrank.gif)

# Install Package
With the native [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html), you can add `https://github.com/Preliy/SliderCrank.git#upm` via the Git URL or modify the `Packages/manifest.json` file directly.

```json
    {
        "dependencies": {
            "com.preliy.chain": "https://github.com/Preliy/SliderCrank.git#upm"
        }
    }
```

# How to use
Add `SliderCrank.cs` component to GameObject.
Transform of the GameObject represents the slider axis, and the slider moves along the Z-axis.

![SliderCrankInspector.png](Documentation%2FSliderCrankInspector.png)
![SliderCrank.png](Documentation%2FSliderCrank.png)

+ The parameter `Length` (L) is the length between the wheel pin and the sliding pin
+ If `Use Negative Solution` is true, a negative solution is used for the slider position
+ `Pin` and `Slider` are references to corresponding transforms
+ Select the `Update Type` to perform the calculation in a specific loop
+ If `Execute in Editor` is true, the calculation is executed and the position of the slider is changed
+ Use `Gizmos` parameter to display the gizmos

# Demo
In `Samples/Demo` you can find demo scene and look at the example mechanism.




