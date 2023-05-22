workspace {

    model {
        user = person "Użytkownik" "Użytkownik aplikacji do zarządzania nawykami"
        habitSystem = softwareSystem "HabitHook system" "System śledzący nawyki i ich status oraz pozwalający na użytkownikowi na interakcje z tymi danymi" {
            singlePageApp = container "Aplikacja typu SPA" "Zapewnia dostępną funckjonalność zarządzania nawykami i przeglądanie ich statusu przez przeglądarkę" "Blazor WASM" "Web Browser"
            apiBackend = container "Aplikacja API" "Udostępnia funkcję programatycznego zarządzania nawykami i sprawdzania ich stanu przez JSON+HTTPS" "ASP.NET Core"
            database = container "Baza danych" "Przechowuje dane związane z nawykami i ich integracjami" "T-SQL Schema" "Database"
        }
        externalWebhookSource = softwareSystem "Żródło webhook" "Zewnętrzne źródło danych o stanie nawyku zintegrowane przez Webhook" "Existing System"
        

        user -> habitSystem "Zarządza nawykami i sprawdza obecny stan ich wykonania, dodaje zewnętrzne źródła"
        externalWebhookSource -> habitSystem "Informuje o zdarzeniach związanych z nawykami" Webhook

        # relationships between containters and people
        user -> singlePageApp "Przegląda i modyfikuje dane używając"

        # relationships between containers and software systems
        externalWebhookSource -> apiBackend "Wysyła żądania o aktualizacji stanu związanymi z nawykami" "JSON/HTTPS"

        # relationshpis between containers and other containers
        singlePageApp -> apiBackend "Wysyła żądania do API" " JSON/HTTPS"
        apiBackend -> database "Odczytuje z / zapisuje do" "SQL/TCP"
    }

    views {
        systemContext habitSystem "DiagramKontekstu" {
            include *
            autoLayout
        }
        container habitSystem "Kontenery" {
            include *
            autoLayout
        }
        theme default
        styles {
            element "Existing System" {
                background #999999
                color #ffffff
            }
            element "Web Browser" {
                shape WebBrowser
            }
            element "Database" {
                shape Cylinder
            }
        }
    }
    
}