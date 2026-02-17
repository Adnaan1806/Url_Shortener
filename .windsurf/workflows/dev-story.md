# Development Story
## Story 1.1 — Implement URL Shortening API

---

## Objective

Develop the core API endpoint to accept a long URL, generate a unique short code, store the mapping in the database, and return the shortened URL.

---

## API Specification

### Endpoint

POST /shorten

---

### Request Body

```json
{
  "url": "https://example.com/very-long-link"
}
