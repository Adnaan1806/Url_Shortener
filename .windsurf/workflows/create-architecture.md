# Architecture Document: High-Scale URL Shortener

---

## 1. Architecture Overview

The High-Scale URL Shortener is designed as a distributed, horizontally scalable system capable of handling over 10 million daily redirects with low latency and high availability.

The architecture uses stateless application servers, Redis caching, PostgreSQL database sharding, and load balancing to ensure performance at scale.

---

## 2. High-Level Architecture

### Components

- Client Applications (Web / Mobile / API)
- Load Balancer
- Stateless Application Servers
- Redis Cache Layer
- Sharded PostgreSQL Databases

---

### Request Flow — Read (Redirect)

1. Client requests short URL  
2. Load balancer routes request  
3. App server checks Redis cache  
4. Cache hit → Redirect  
5. Cache miss → Query DB shard  
6. Response cached  
7. Redirect returned  

---

### Request Flow — Write (Shorten)

1. Client submits long URL  
2. Load balancer routes request  
3. App server generates short code  
4. Shard router selects DB shard  
5. Mapping stored  
6. Cache invalidated/updated  
7. Short URL returned  

---

## 3. Component Design

### 3.1 Load Balancer

- Distributes traffic using round-robin
- Ensures high availability
- Prevents server overload

---

### 3.2 Application Servers

- Stateless design
- Horizontally scalable
- Handles API logic
- Generates short codes

---

### 3.3 Caching Layer (Redis)

**Pattern:** Cache-Aside

Flow:

- Check cache first
- On miss → Query DB
- Store hot URLs

**TTL:** 5 minutes – 1 hour

**Benefits:**

- Reduces DB load by 80%+
- Improves redirect latency

---

### 3.4 Database Layer

**Database:** PostgreSQL

**Storage Strategy:** Sharding

Reason:

- Handles massive datasets
- Prevents single DB overload

---

## 4. Sharding Strategy

### Method: Hash-Based Sharding

Shard key:

short_code

Algorithm:

hash(short_code) % N


Example:

| Short Code | Hash | Shard |
|------------|------|--------|
| abc123 | 91231 | Shard 1 |
| xyz789 | 44211 | Shard 2 |

---

### Why Hash Sharding?

- Even data distribution
- No hotspotting
- Predictable routing

---

## 5. Data Model

### Table: ShortUrls

| Field | Type |
|------|------|
| id | UUID |
| short_code | varchar |
| original_url | text |
| created_at | timestamp |
| expiry_date | timestamp |

---

## 6. Scalability Design

### Horizontal Scaling

- Add more app servers
- No session affinity needed
- Stateless architecture

---

### Vertical Scaling (Limited)

- Increase CPU/RAM if needed
- Short-term scaling solution

---

## 7. Performance Optimization

- Redis caching
- Indexed short_code column
- Connection pooling
- CDN integration (future)

---

## 8. Capacity Estimation

| Metric | Value |
|-------|-------|
| Daily redirects | 10M |
| Writes/day | 100K |
| Read:Write ratio | 100:1 |
| Peak QPS | ~230 |
| Storage (5 yrs) | ~91 GB |
| Cache size | ~1 GB |

---

## 9. Availability & Fault Tolerance

- Load balancer failover
- Multiple app instances
- Redis optional fallback
- DB replication (future)

---

## 10. CAP Theorem Consideration

Chosen model: **AP (Availability + Partition Tolerance)**

Reason:

- Redirect availability prioritized
- Eventual consistency acceptable

---

## 11. Failure Scenarios

| Failure | Handling |
|--------|-----------|
| Cache down | Query DB |
| Shard down | Retry/replica |
| Traffic spike | Auto-scale |
| Code collision | Regenerate code |

---

## 12. Security Considerations

- Input validation
- URL sanitization
- Rate limiting
- HTTPS redirects

---

## 13. Deployment Architecture

- Docker containers
- PostgreSQL shards
- Redis container
- App server containers

---

## 14. Future Enhancements

- Multi-region deployment
- Analytics pipeline
- Custom domains
- Event streaming (Kafka)

---

