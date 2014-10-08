namespace JpMdDiet

[<ReflectedDefinition>]
type IngredientCategory =
    | Meat
    | Fish
    | Vegetable
    | Fruit
    | Cereal

[<ReflectedDefinition>]
type RecipeVerb =
    | Mix
    | Heat

[<ReflectedDefinition>]
type RecipeTool =
    | Frypan 

[<ReflectedDefinition>]
type Ingredient = {
    name: string
    category: IngredientCategory
}

[<ReflectedDefinition>]
type IngredientWithMeasure = {
    ingredient: Ingredient
    quantity: int // TODO: replace
}

[<ReflectedDefinition>]
type RecipeStep = {
    verb: RecipeVerb
    ingredients: IngredientWithMeasure[]
    tools: RecipeTool[]
    time: float // TODO: replace? make it more restrictive?
}

[<ReflectedDefinition>]
type Recipe = {
    name: string // TODO: constraints
    author: string
    isPublic: bool
    rating: float
    picture: byte[]
    steps: RecipeStep[]
}
    
