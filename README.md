# InteractiveBlazorAuth

This WIP demo shows one approach to authentication with Blazor's new interactive render modes.

The idea is that in interactive wasm mode, we use a BFF, while in SSR and interactive server modes, we use a server side store for tokens. In order to get these two different behaviors, we abstract data retrieval behind an interface, and register different implementations in the server and client projects. Both code paths ultimately rely on the same abstractions within Duende.AccessTokenManagement, and thus token management is shared.

