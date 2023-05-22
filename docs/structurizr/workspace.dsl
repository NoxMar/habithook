workspace {

    model {
        user = person "Użytkownik" "Użytkownik aplikacji do zarządzania nawykami"
        habitSystem = softwareSystem "HabitHook system" "System śledzący nawyki i ich status oraz pozwalający na użytkownikowi na interakcje z tymi danymi"
        externalWebhookSource = softwareSystem "Żródło webhook" "Zewnętrzne źródło danych o stanie nawyku zintegrowane przez Webhook" "Existing System"
        

        user -> habitSystem "Zarządza nawykami i sprawdza obecny stan ich wykonania, dodaje zewnętrzne źródła"
        externalWebhookSource -> habitSystem "Informuje o zdarzeniach związanych z nawykami" Webhook
    }

    views {
        systemContext habitSystem "DiagramKontekstu" {
            include *
            autoLayout
        }
        theme default
        styles {
            element "Existing System" {
                background #999999
                color #ffffff
            }
        }
    }
    
}