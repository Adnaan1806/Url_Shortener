# Epics & User Stories
## Project: High-Scale URL Shortener

---

# EPIC 1 — URL Shortening Service

### Goal
Enable users to create short URLs from long links.

---

### Story 1.1 — Create Short URL

**As a** user  
**I want** to submit a long URL  
**So that** I receive a shortened link  

**Acceptance Criteria**

- Valid URL required
- Unique short code generated
- Mapping stored in database

---

### Story 1.2 — Generate Short Code

**Acceptance Criteria**

- Code length ~7 characters
- Base62 encoding used
- No collisions allowed

---

### Story 1.3 — Custom Alias

**Acceptance Criteria**

- User-defined alias supported
- Alias must be unique
- Reserved keywords restricted

---

# EPIC 2 — Redirect Service

### Goal
Redirect short URLs to original links instantly.

---

### Story 2.1 — Redirect Using Short Code

**Acceptance Criteria**

- GET /{code} endpoint
- Returns HTTP 302 redirect
- Works under <100ms latency

---

### Story 2.2 — Cache Lookup

**Acceptance Criteria**

- Redis checked first
- Cache hit → redirect
- Cache miss → DB lookup

---

# EPIC 3 — Caching Layer

### Goal
Improve redirect performance and reduce DB load.

---

### Story 3.1 — Implement Redis Cache

**Acceptance Criteria**

- Cache-aside pattern used
- Store hot URLs
- TTL configured

---

### Story 3.2 — Cache Invalidation

**Acceptance Criteria**

- Expired URLs removed
- Updates invalidate cache
- LRU eviction supported

---

# EPIC 4 — Database Sharding

### Goal
Distribute data across multiple DB instances.

---

### Story 4.1 — Implement Shard Router

**Acceptance Criteria**

- Hash(short_code) % N routing
- Even data distribution

---

### Story 4.2 — Write to Correct Shard

**Acceptance Criteria**

- URL stored in selected shard
- Write latency optimized

---

### Story 4.3 — Read from Correct Shard

**Acceptance Criteria**

- Redirect queries routed properly
- Fallback handling exists

---

# EPIC 5 — API Layer

### Goal
Expose system functionality via REST APIs.

---

### Story 5.1 — POST /shorten

**Acceptance Criteria**

- Accepts long URL
- Returns short code
- Stores mapping

---

### Story 5.2 — GET /{code}

**Acceptance Criteria**

- Resolves original URL
- Performs redirect

---

# EPIC 6 — Load Testing

### Goal
Validate system performance under traffic.

---

### Story 6.1 — k6 Test Scripts

**Acceptance Criteria**

- Simulate 10M daily traffic
- Measure QPS & latency

---

### Story 6.2 — Benchmark Reporting

**Acceptance Criteria**

- Capture P95 latency
- Identify bottlenecks

---

# EPIC 7 — Deployment & Infrastructure

### Goal
Deploy scalable production-ready system.

---

### Story 7.1 — Docker Setup

**Acceptance Criteria**

- App containerized
- Redis containerized
- PostgreSQL shards containerized

---

### Story 7.2 — Load Balancer Config

**Acceptance Criteria**

- Traffic evenly distributed
- Health checks enabled

---

# EPIC 8 — Monitoring & Reliability

### Goal
Ensure system observability and fault tolerance.

---

### Story 8.1 — Logging

**Acceptance Criteria**

- Request logs captured
- Error logs stored

---

### Story 8.2 — Failure Handling

**Acceptance Criteria**

- Cache fallback to DB
- Retry shard failures

---

# Development Priority Order

1. URL Shortening API  
2. Redirect Service  
3. Database Integration  
4. Redis Caching  
5. Sharding Router  
6. Load Testing  
7. Deployment  

---
