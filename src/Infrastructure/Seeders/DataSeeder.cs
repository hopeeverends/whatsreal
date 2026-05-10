namespace WhatsReal.Infrastructure.Seeders;

using WhatsReal.Domain.Entities;

/// <summary>
/// Seeds the repositories with realistic mock data.
/// Provides 25 properties, 5 agents across multiple locations and property types.
/// </summary>
public static class DataSeeder
{
    public static List<Agent> GetAgents()
    {
        return new List<Agent>
        {
            new Agent
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FirstName = "Sarah",
                LastName = "Johnson",
                Email = "sarah.johnson@whatsreal.com",
                PhoneNumber = "+12125551001",
                Bio = "Expert real estate agent with 10+ years of experience in luxury apartments and penthouses.",
                ImageUrl = "https://api.placeholder.com/agents/sarah-johnson.jpg"
            },
            new Agent
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FirstName = "Michael",
                LastName = "Chen",
                Email = "michael.chen@whatsreal.com",
                PhoneNumber = "+12125551002",
                Bio = "Specialist in family homes and townhouses. Strong track record in suburban markets.",
                ImageUrl = "https://api.placeholder.com/agents/michael-chen.jpg"
            },
            new Agent
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                FirstName = "Emma",
                LastName = "Williams",
                Email = "emma.williams@whatsreal.com",
                PhoneNumber = "+12125551003",
                Bio = "Downtown specialist. Award-winning agent focused on urban condos and studios.",
                ImageUrl = "https://api.placeholder.com/agents/emma-williams.jpg"
            },
            new Agent
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                FirstName = "David",
                LastName = "Rodriguez",
                Email = "david.rodriguez@whatsreal.com",
                PhoneNumber = "+12125551004",
                Bio = "Waterfront properties specialist. Expert negotiator with proven success in premium estates.",
                ImageUrl = "https://api.placeholder.com/agents/david-rodriguez.jpg"
            },
            new Agent
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                FirstName = "Jessica",
                LastName = "Lee",
                Email = "jessica.lee@whatsreal.com",
                PhoneNumber = "+12125551005",
                Bio = "Investment property expert. Focused on rental properties and portfolio building.",
                ImageUrl = "https://api.placeholder.com/agents/jessica-lee.jpg"
            }
        };
    }

    public static List<Property> GetProperties()
    {
        var agents = GetAgents();
        var properties = new List<Property>();
        
        // New York - Manhattan Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Luxury Penthouse in Midtown",
                Description = "Stunning penthouse with panoramic views of Central Park and Manhattan skyline. Features 4 bedrooms, premium finishes, and private terrace.",
                Price = 8500000M,
                Type = PropertyType.Penthouse,
                Bedrooms = 4,
                Bathrooms = 3,
                IsFurnished = true,
                SquareFeet = 4500,
                Address = "555 5th Avenue",
                City = "New York",
                State = "NY",
                ZipCode = "10022",
                Latitude = 40.7614M,
                Longitude = -73.9776M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800",
                    "https://images.unsplash.com/photo-1512917774080-9264f475eabf?w=800",
                    "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=800"
                },
                AgentId = agents[0].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Modern Studio in Times Square",
                Description = "Compact modern studio with contemporary design. Perfect for young professionals. Recently renovated.",
                Price = 450000M,
                Type = PropertyType.Studio,
                Bedrooms = 1,
                Bathrooms = 1,
                IsFurnished = false,
                SquareFeet = 450,
                Address = "1500 Broadway",
                City = "New York",
                State = "NY",
                ZipCode = "10036",
                Latitude = 40.7580M,
                Longitude = -73.9855M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[2].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Two-Bedroom Upper East Side Apartment",
                Description = "Classic pre-war apartment with high ceilings and original hardwood floors. Located in prestigious Upper East Side.",
                Price = 2100000M,
                Type = PropertyType.Apartment,
                Bedrooms = 2,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 1800,
                Address = "740 Park Avenue",
                City = "New York",
                State = "NY",
                ZipCode = "10021",
                Latitude = 40.7714M,
                Longitude = -73.9776M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1512917774080-9264f475eabf?w=800"
                },
                AgentId = agents[0].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Downtown Loft with City Views",
                Description = "Spacious industrial loft converted into modern living space. Open floor plan with high ceilings and natural light.",
                Price = 1650000M,
                Type = PropertyType.Condo,
                Bedrooms = 3,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 2200,
                Address = "123 Spring Street",
                City = "New York",
                State = "NY",
                ZipCode = "10012",
                Latitude = 40.7250M,
                Longitude = -73.9987M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[2].Id
            }
        });

        // Brooklyn Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Brownstone in Brooklyn Heights",
                Description = "Beautifully restored brownstone with garden. 4 stories, perfect for families. Close to schools and parks.",
                Price = 3200000M,
                Type = PropertyType.House,
                Bedrooms = 4,
                Bathrooms = 3,
                IsFurnished = false,
                SquareFeet = 3500,
                Address = "250 Montague Street",
                City = "Brooklyn",
                State = "NY",
                ZipCode = "11201",
                Latitude = 40.6949M,
                Longitude = -73.9913M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[1].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Williamsburg Condo with Rooftop",
                Description = "Modern condo in trendy Williamsburg. Includes rooftop access, gym, and doorman services.",
                Price = 980000M,
                Type = PropertyType.Condo,
                Bedrooms = 2,
                Bathrooms = 2,
                IsFurnished = true,
                SquareFeet = 1400,
                Address = "100 Kent Avenue",
                City = "Brooklyn",
                State = "NY",
                ZipCode = "11249",
                Latitude = 40.7176M,
                Longitude = -73.9571M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[2].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Park Slope Townhouse",
                Description = "Charming townhouse with exposed brick and fireplace. Steps from Prospect Park.",
                Price = 2750000M,
                Type = PropertyType.Townhouse,
                Bedrooms = 3,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 2100,
                Address = "750 Prospect Park West",
                City = "Brooklyn",
                State = "NY",
                ZipCode = "11215",
                Latitude = 40.6622M,
                Longitude = -73.9756M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[1].Id
            }
        });

        // Los Angeles Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Beverly Hills Modern Mansion",
                Description = "Stunning contemporary mansion with infinity pool, smart home system, and guest house. Hollywood Hills views.",
                Price = 12500000M,
                Type = PropertyType.Villa,
                Bedrooms = 5,
                Bathrooms = 6,
                IsFurnished = true,
                SquareFeet = 8000,
                Address = "1010 North Rexford Drive",
                City = "Beverly Hills",
                State = "CA",
                ZipCode = "90210",
                Latitude = 34.0784M,
                Longitude = -118.4071M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1512917774080-9264f475eabf?w=800",
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[3].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Santa Monica Beach House",
                Description = "Oceanfront property with direct beach access. 3 bedrooms, spacious deck with coastal views.",
                Price = 5800000M,
                Type = PropertyType.House,
                Bedrooms = 3,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 3200,
                Address = "200 Pacific Coast Highway",
                City = "Santa Monica",
                State = "CA",
                ZipCode = "90402",
                Latitude = 34.0195M,
                Longitude = -118.4912M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[3].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Westwood Contemporary Condo",
                Description = "Sleek 2-bedroom condo with community amenities. Near UCLA campus and Westwood shopping.",
                Price = 750000M,
                Type = PropertyType.Condo,
                Bedrooms = 2,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 1200,
                Address = "10950 Wilshire Boulevard",
                City = "Westwood",
                State = "CA",
                ZipCode = "90024",
                Latitude = 34.0696M,
                Longitude = -118.4385M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[1].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Studio in Downtown LA",
                Description = "Modern studio in revitalized DTLA. Building has rooftop bar, fitness center, and concierge.",
                Price = 385000M,
                Type = PropertyType.Studio,
                Bedrooms = 1,
                Bathrooms = 1,
                IsFurnished = true,
                SquareFeet = 550,
                Address = "600 South Spring Street",
                City = "Los Angeles",
                State = "CA",
                ZipCode = "90014",
                Latitude = 34.0442M,
                Longitude = -118.2523M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[2].Id
            }
        });

        // Miami Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Wynwood Loft with Art Studio",
                Description = "Artist loft in vibrant Wynwood arts district. High ceilings, natural light, private outdoor space.",
                Price = 550000M,
                Type = PropertyType.Loft,
                Bedrooms = 2,
                Bathrooms = 1,
                IsFurnished = false,
                SquareFeet = 1400,
                Address = "270 NW 23rd Street",
                City = "Miami",
                State = "FL",
                ZipCode = "33127",
                Latitude = 25.7945M,
                Longitude = -80.2057M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[4].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Brickell Waterfront Penthouse",
                Description = "Ultra-luxury penthouse with Biscayne Bay views. 3 bedrooms, smart home, private elevator.",
                Price = 4200000M,
                Type = PropertyType.Penthouse,
                Bedrooms = 3,
                Bathrooms = 3,
                IsFurnished = true,
                SquareFeet = 3000,
                Address = "1200 Brickell Avenue",
                City = "Miami",
                State = "FL",
                ZipCode = "33131",
                Latitude = 25.7579M,
                Longitude = -80.1903M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1512917774080-9264f475eabf?w=800"
                },
                AgentId = agents[3].Id
            }
        });

        // San Francisco Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Mission District Apartment",
                Description = "Charming apartment in vibrant Mission District. Close to restaurants, galleries, and tech hub.",
                Price = 1200000M,
                Type = PropertyType.Apartment,
                Bedrooms = 2,
                Bathrooms = 1,
                IsFurnished = false,
                SquareFeet = 950,
                Address = "3200 Mission Street",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94110",
                Latitude = 37.7554M,
                Longitude = -122.4126M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[4].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Pacific Heights Victorian",
                Description = "Iconic Victorian home in prestigious Pacific Heights. Grand dame architecture, ornate details.",
                Price = 3800000M,
                Type = PropertyType.House,
                Bedrooms = 4,
                Bathrooms = 3,
                IsFurnished = false,
                SquareFeet = 3600,
                Address = "2000 Fillmore Street",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94115",
                Latitude = 37.7885M,
                Longitude = -122.4365M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[0].Id
            }
        });

        // Seattle Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Queen Anne Modern Home",
                Description = "Modern home in sought-after Queen Anne neighborhood. Space Needle views, newly renovated.",
                Price = 1850000M,
                Type = PropertyType.House,
                Bedrooms = 3,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 2200,
                Address = "4500 Interbay Avenue North",
                City = "Seattle",
                State = "WA",
                ZipCode = "98109",
                Latitude = 47.6405M,
                Longitude = -122.3493M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[1].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Capitol Hill Condo",
                Description = "Hip condo in artsy Capitol Hill. Close to bars, restaurants, and Pike Place Market.",
                Price = 625000M,
                Type = PropertyType.Condo,
                Bedrooms = 2,
                Bathrooms = 1,
                IsFurnished = false,
                SquareFeet = 1000,
                Address = "1525 11th Avenue",
                City = "Seattle",
                State = "WA",
                ZipCode = "98122",
                Latitude = 47.6205M,
                Longitude = -122.3212M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[2].Id
            }
        });

        // Boston Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Back Bay Brownstone",
                Description = "Historic brownstone in prestigious Back Bay. Original details, updated kitchen and bathrooms.",
                Price = 2900000M,
                Type = PropertyType.Townhouse,
                Bedrooms = 4,
                Bathrooms = 3,
                IsFurnished = false,
                SquareFeet = 2800,
                Address = "250 Commonwealth Avenue",
                City = "Boston",
                State = "MA",
                ZipCode = "02215",
                Latitude = 42.3497M,
                Longitude = -71.0783M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[0].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Beacon Hill Apartment",
                Description = "Charming apartment in historic Beacon Hill. Gas lamps, brick sidewalks, community feel.",
                Price = 875000M,
                Type = PropertyType.Apartment,
                Bedrooms = 2,
                Bathrooms = 1,
                IsFurnished = false,
                SquareFeet = 1100,
                Address = "74 Pinckney Street",
                City = "Boston",
                State = "MA",
                ZipCode = "02114",
                Latitude = 42.3605M,
                Longitude = -71.0693M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                },
                AgentId = agents[1].Id
            }
        });

        // Chicago Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Lincoln Park Brownstone",
                Description = "Upscale brownstone in trendy Lincoln Park. Renovated with modern amenities, private patio.",
                Price = 2100000M,
                Type = PropertyType.Townhouse,
                Bedrooms = 3,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 2000,
                Address = "2340 North Larrabee Street",
                City = "Chicago",
                State = "IL",
                ZipCode = "60614",
                Latitude = 41.9159M,
                Longitude = -87.6397M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1493857671505-72967e2e2760?w=800"
                },
                AgentId = agents[1].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Downtown Chicago Penthouse",
                Description = "Luxury penthouse with Lake Michigan and skyline views. State-of-the-art building amenities.",
                Price = 3500000M,
                Type = PropertyType.Penthouse,
                Bedrooms = 3,
                Bathrooms = 3,
                IsFurnished = true,
                SquareFeet = 2600,
                Address = "875 North Michigan Avenue",
                City = "Chicago",
                State = "IL",
                ZipCode = "60611",
                Latitude = 41.8902M,
                Longitude = -87.6242M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1512917774080-9264f475eabf?w=800"
                },
                AgentId = agents[3].Id
            }
        });

        // Austin Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "East Austin Smart Townhome",
                Description = "Energy-efficient townhome with solar readiness, private garage, and flexible office nook near restaurants and transit.",
                Price = 695000M,
                Type = PropertyType.Townhouse,
                Bedrooms = 3,
                Bathrooms = 3,
                IsFurnished = false,
                SquareFeet = 1850,
                Address = "1801 East 6th Street",
                City = "Austin",
                State = "TX",
                ZipCode = "78702",
                Latitude = 30.2622M,
                Longitude = -97.7231M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800",
                    "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800"
                },
                AgentId = agents[4].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Downtown Austin Furnished Condo",
                Description = "Turnkey condo with balcony, coworking lounge, pool, and easy access to the central business district.",
                Price = 825000M,
                Type = PropertyType.Condo,
                Bedrooms = 2,
                Bathrooms = 2,
                IsFurnished = true,
                SquareFeet = 1180,
                Address = "301 West Avenue",
                City = "Austin",
                State = "TX",
                ZipCode = "78701",
                Latitude = 30.2676M,
                Longitude = -97.7502M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800"
                },
                AgentId = agents[2].Id
            }
        });

        // Denver Properties
        properties.AddRange(new[]
        {
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "LoHi Apartment with Mountain Views",
                Description = "Bright apartment with balcony, secure parking, and western views close to cafes, parks, and downtown Denver.",
                Price = 615000M,
                Type = PropertyType.Apartment,
                Bedrooms = 2,
                Bathrooms = 2,
                IsFurnished = false,
                SquareFeet = 1050,
                Address = "2555 17th Street",
                City = "Denver",
                State = "CO",
                ZipCode = "80211",
                Latitude = 39.7591M,
                Longitude = -105.0115M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1600566753190-17f0baa2a6c3?w=800"
                },
                AgentId = agents[1].Id
            },
            new Property
            {
                Id = Guid.NewGuid(),
                Title = "Cherry Creek Luxury Villa",
                Description = "Private villa with chef kitchen, landscaped courtyard, and premium finishes in a walkable Denver neighborhood.",
                Price = 2450000M,
                Type = PropertyType.Villa,
                Bedrooms = 4,
                Bathrooms = 4,
                IsFurnished = true,
                SquareFeet = 3900,
                Address = "250 Columbine Street",
                City = "Denver",
                State = "CO",
                ZipCode = "80206",
                Latitude = 39.7207M,
                Longitude = -104.9564M,
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1600573472550-8090b5e0745e?w=800",
                    "https://images.unsplash.com/photo-1600585154526-990dced4db0d?w=800"
                },
                AgentId = agents[3].Id
            }
        });

        for (var i = 0; i < properties.Count; i++)
        {
            properties[i].Id = Guid.Parse($"00000000-0000-0000-0000-{(i + 1).ToString().PadLeft(12, '0')}");
        }

        return properties;
    }
}
