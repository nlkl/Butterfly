![Butterfly](https://raw.githubusercontent.com/nlkl/Butterfly/master/misc/logo.png)

Beautifully typed items for Sitecore

## Overview

**Note: Butterfly is a work in progress, and still in the early stages of development**

Butterfly is a simpler and safer object model for Sitecore items. 

Butterfly allows you to easily construct safe and convenient object models for your Sitecore items, without stuffing specific architectural choices down your throat. Butterfly encourages a safe object model, and uses [Optional](https://github.com/nlkl/Butterfly) to avoid passing null references around.

Butterfly tries to combine the good parts of [Synthesis](https://github.com/kamsar/Synthesis) and [Fortis](https://github.com/Fortis-Collection/fortis), but sticks to a minimalist and less opinionated (apart from the "safe" part, maybe) approach. Butterfly doesn't care how you generate your models, nor does it enforce a specific way of instantiating them. Instead, Butterfly gives you a nicer domain model for Sitecore items and fields - the rest is up to you!

## Dependencies

To build the project, place the following assemblies into a "lib" folder in the repository root:

* Sitecore.Kernel.dll
