# Games Shop Web API
API for an e-commerce application to sell games.

##  Table of Contents
* [`/auth`](#auth)
  * [Authentication](#authentication)
  * [Registration](#registration)
* [`/user`](#user)
  * [Update user profile](#update-user-profile)
  * [Update user password](#update-user-password)
  * [Get user profile](#get-user-profile)
* [`/games`](#games)
  * [Get top platforms](#get-top-platforms)
  * [Search games](#search-games)
  * [Get game](#get-game)
  * [Create game](#create-game)
  * [Update game](#update-game)
  * [Remove game](#remove-game)
  * [Edit rating](#edit-rating)
* [`/products`](#products)
  * [Get products list](#get-products-list)
* [`/orders`](#orders)
  * [Create order](#create-order)
  * [Get orders](#get-orders)
  * [Remove products from order](#remove-products-from-order)
  * [Buy order](#buy-order)

## `/auth`

### Authentication

#### Endpoint
**`POST`** `/api/auth/signIn`

#### Description
User authentication endpoint.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `Email` | string |
| `Password` | string |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |
| `401` | Unsuccessful authentication |

### Registration

#### Endpoint
**`POST`** `/api/auth/signUp`

#### Description
User registration endpoint.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `Email` | string |
| `Password` | string |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `201` | Ok |
| `400` | Unsuccessful registration |

## `/user`

### Update user profile

#### Endpoint
**`PUT`** `/api/user`

#### Description
Endpoint to update user profile.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `UserName` | string |
| `AddressDelivery` | string |
| `PhoneNumber` | string |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Update user password

#### Endpoint
**`POST`** `/api/user/password`

#### Description
Endpoint to update user password.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `Password` | string |
| `NewPassword` | string |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `204` | Ok |

### Get user profile

#### Endpoint
**`GET`** `/api/user`

#### Description
Endpoint to get user profile.

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

## `/games`

### Get top platforms

#### Endpoint
**`GET`** `/api/games/topPlatforms`

#### Description
Endpoint to get info about 3 top popular platforms.

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Search games

#### Endpoint
**`GET`** `/api/games/search?term={term}&limit={limit}`

#### Description
Endpoint to search games.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `Term` | string |
| `Limit` | int |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Get game

#### Endpoint
**`GET`** `/api/games/id/{id}`

#### Description
Endpoint to get game by id.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `Id` | Guid |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Create game

#### Endpoint
**`POST`** `/api/games`

#### Description
Endpoint to create game.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `Id` | Guid |
| `Name` | string |
| `Platform` | PlatformTypeEnum? |
| `DateCreated` | DateTime? |
| `Rating` | int? |
| `Genre` | string |
| `AgeRating` | AgeRatingEnum? |
| `Logo` | IFormFile |
| `Background` | IFormFile |
| `Price` | float? |
| `Count` | int? |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `201` | Ok |

### Get game

#### Endpoint
**`GET`** `/api/games/id/{id}`

#### Description
Endpoint to get game by id.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `Id` | Guid |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Update game

#### Endpoint
**`PUT`** `/api/games`

#### Description
Endpoint to update game.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `Id` | Guid |
| `Name` | string |
| `Platform` | PlatformTypeEnum? |
| `DateCreated` | DateTime? |
| `Rating` | int? |
| `Genre` | string |
| `AgeRating` | AgeRatingEnum? |
| `Logo` | IFormFile |
| `Background` | IFormFile |
| `Price` | float? |
| `Count` | int? |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Remove game

#### Endpoint
**`DELETE`** `/api/games/id/{id}`

#### Description
Endpoint to remove game.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `Id` | Guid |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `204` | Ok |

### Edit rating

#### Endpoint
**`POST`** `/api/games/rating`

#### Description
Endpoint to edit game's rating.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `ProductId` | Guid |
| `Rating` | int |
| `UserId` | string |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

## `/products`

### Get products list

#### Endpoint
**`GET`** `/api/products/list?pageNumber={pageNumber}&genre={genre}&ageRatig={ageRatig}&totalRatingOrder={totalRatingOrder}&priceOrder={priceOrder}`

#### Description
Endpoint to get products list.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `PageNumber` | int |
| `Genre` | string |
| `AgeRatig` | AgeRatingEnum? |
| `TotalRatingOrder` | OrderBy? |
| `PriceOrder` | OrderBy? |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

## `/orders`

### Create order

#### Endpoint
**`POST`** `/api/orders`

#### Description
Endpoint to create order.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `ProductId` | Guid |
| `Amount` | int |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `201` | Ok |

### Get orders

#### Endpoint
**`GET`** `/api/orders?orderId={orderId}`

#### Description
Endpoint to get orders.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `OrderId` | Guid? |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `200` | Ok |

### Remove products from order

#### Endpoint
**`DELETE`** `/api/orders?orderId={orderId}`

#### Description
Endpoint to remove products from order.

#### Request parameters
**Field** | **Type** |
|:--------|:---------|
| `OrderId` | Guid |

#### Body
**Field** | **Type** |
|:--------|:---------|
| `ProductsList` | List\<Guid\> |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `204` | Ok |

### Buy order

#### Endpoint
**`POST`** `/api/orders/buy`

#### Description
Endpoint to buy products from order.

#### Body
**Field** | **Type** |
|:--------|:---------|
| `OrderId` | Guid |

#### Response
**Status code** | **Type** |
|:--------|:---------|
| `204` | Ok |
