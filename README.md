# VR Shopping
https://user-images.githubusercontent.com/36990593/120388997-257f2900-c32c-11eb-818d-ce78595c95af.mp4

Create your own virtual shopping experience. All products are streamed from an API including the meshes, textures, materials and the metadata.

## Product API
`https://vr-shopping.azurewebsites.net/api/QueryItems`

Each item along with name, description and othermetada carries additional visual data, which consists source mesh and materials. All materials have standard default shader.  
```json
{
    "Id": 1,
    "Name": "Fluffies Bread",
    "Description": "Fluffies bread has been bringing delicious tasting, soft textured bread to the table and pleasing generations of families in the process!",
    "Category": "other",
    "Price": 7.99,
    "Weight": "566.99 g",
    "NutritionFacts": {
      "Calories": 1400.0,
      "Fats": 15.0,
      "Carbohydrates": 290.0,
      "Proteins": 40.0,
      "FatsDaily": 0.230769232,
      "CarbohydratesDaily": 0.966666639,
      "ProteinsDaily": 0.3
    },
    "Bounds": {
      "Center": {
        "x": -0.026195392,
        "y": 0.0575647131,
        "z": -3.57627869E-07
      },
      "Extents": {
        "x": 0.163743958,
        "y": 0.06052745,
        "z": 0.06537649
      }
    },
    "Visuals": [
      {
        "Source": "https://vrshopping.blob.core.windows.net/items/SMGP_PRE_Bread_wrapped_1024.mesh",
        "Materials": [
          {
            "IntProperties": {
              "_SrcBlend": 1,
              "_DstBlend": 10,
              "_ZWrite": 0
            },
            "FloatProperties": {
              "_Mode": 3.0,
              "_Glossiness": 0.699
            },
            "BoolProperties": {
              "_ALPHATEST_ON": false,
              "_ALPHABLEND_ON": false,
              "_ALPHAPREMULTIPLY_ON": true,
              "_METALLICGLOSSMAP": true,
              "_EMISSION": true,
              "_SPECULARHIGHLIGHTS_OFF": false,
              "_GLOSSYREFLECTIONS_OFF": false
            },
            "ColorProperties": {},
            "TextureProperties": {
              "_MainTex": {
                "Source": "https://vrshopping.blob.core.windows.net/items/TEX_Transparent_items_AA_1024.png"
              },
              "_MetallicGlossMap": {
                "Source": "https://vrshopping.blob.core.windows.net/items/TEX_Transparent_items_MS_512.png"
              }
            },
            "RenderQueue": 3000
          }
        ]
      },
      ...
    ]
  }
```

Each item has

# vr-shopping

Minimal oculus framework (without lipsync,samples,spatializer)
Visuals format?
Loading in background thread issues
