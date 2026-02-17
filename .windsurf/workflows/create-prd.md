# Product Requirements Document (PRD)
## Project: High-Scale URL Shortener

---

## 1. Executive Summary

The High-Scale URL Shortener is a distributed system designed to generate short links and handle over 10 million daily redirects with high availability, low latency, and horizontal scalability using .NET 8, PostgreSQL sharding, and Redis caching.

---

## 2. Project Scope & Objectives

### Objectives

- Build a scalable URL shortening platform
- Support 10M+ redirects/day
- Ensure <100ms latency
- Maintain 99.9% availability
- Store URL mappings for 5 years

### In Scope

- URL shortening
- Redirect service
- Short code generation
- Caching layer
- Database sharding
- Load testing

### Out of Scope (Hackathon Phase)

- Advanced analytics
- Billing systems
- Enterprise SSO

---

## 3. User Personas & Use Cases

### Personas

- Developers sharing documentation
- Marketing teams managing campaigns
- Social media users

### Use Cases

- Shorten long URLs
- Share links
- Redirect users instantly
- Track link usage (future)

---

## 4. Functional Requirements

| ID | Requirement |
|----|-------------|
| FR1 | System must shorten long URLs |
| FR2 | Generate unique short codes |
| FR3 | Redirect users via short URL |
| FR4 | Store URL mappings |
| FR5 | Support custom aliases |
| FR6 | Support link expiration |
| FR7 | Provide analytics (future) |

---

## 5. Non-Functional Requirements

| Category | Requirement |
|---------|-------------|
| Scalability | Handle 10M+ redirects/day |
| Performance | <100ms redirect latency |
| Availability | 99.9% uptime |
| Reliability | No data loss |
| Maintainability | Modular architecture |

---

## 6. User Stories & Acceptance Criteria

### Story 1 — Shorten URL

**As a** user  
**I want** to shorten a long URL  
**So that** I can share it easily  

**Acceptance Criteria**

- Valid URL required
- Unique short code generated
- Mapping stored in DB

---

### Story 2 — Redirect

**As a** visitor  
**I want** to be redirected instantly  
**So that** I reach the original site  

**Acceptance Criteria**

- Redirect <100ms
- Cache checked first
- DB fallback on miss

---

### Story 3 — Custom Alias

**Acceptance Criteria**

- Alias must be unique
- Reserved words blocked

---

## 7. Technical Specifications

### Tech Stack

- Backend: .NET 8 Web API
- Database: PostgreSQL
- Cache: Redis
- Load Testing: k6
- Containerization: Docker

---

### API Endpoints

#### POST /shorten

Request:

```json
{
  "url": "https://example.com"
}
