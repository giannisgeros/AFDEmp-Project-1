[![messaging-app-logo](Images/messagingAppLogo.png)](https://github.com/giannisgeros)

# The Project
This is a console messaging application made in Visual Studio using C#. The user can send and receive messages which are dynamically stored to a database with the help of Entity Framework. Also depending on the user role, the user functionalities can vary (CRUD for Users and RUD for Messages). Finally every move in the app is logged in log.txt file.

## User Roles
| Role        | Funct. 1        | Funct. 2          | Funct. 3         | Funct. 4         | Funct. 5           | Funct. 6       |
|:-----------:|:---------------:|:-----------------:|:----------------:|:----------------:|:------------------:|:--------------:|
| `Admin`     | Send Message    | View Messages     | Create User      | View Users       | Edit User          | Delete User    |
| `Tier3User` | Send Message    | View Messages     | View Any Message | Edit Any Message | Delete Any Message |                |
| `Tier2User` | Send Message    | View Messages     | View Any Message | Edit Any Message |                    |                |
| `Tier1User` | Send Message    | View Messages     | View Any Message |                  |                    |                |
| `Tier0User` | Send Message    | View Messages     |                  |                  |                    |                |

## Installation
  - Install Visual Studio
  - Open the project
  - At the Package Console Manager type: `update-database`
  
_(Entity Framework package is already installed and the 'enable-database' and 'add-migration' commands are already there for you)_

## How to login
Once you have done the above 5 users will be created, one for each role.

| Username | Password | Role      |
|:--------:|:--------:|:---------:|
| Admin    | 12qw!@QW | Admin     |
| Tier3    | 12qw!@QW | Tier3User |
| Tier2    | 12qw!@QW | Tier2User |
| Tier1    | 12qw!@QW | Tier1User |
| Tier0    | 12qw!@QW | Tier0User |

_You can always register as a new user. All new users are registsered with the Tier0User role unless Admin changes that._

## Technologies Used
  - C#
  - Entity Framework
  - LINQ

## Requirements
See [REQUIREMENTS.md](REQUIREMENTS.md) for more information.

## License
Distributed under the MIT license. See [LICENSE.md](LICENSE.md) for more information.