--Role: taskify_auth
DO
$do$
BEGIN
   IF EXISTS (
      SELECT FROM pg_catalog.pg_roles
      WHERE  rolname = 'taskify_auth') THEN

      RAISE NOTICE 'Role "taskify_auth" already exists. Skipping.';
   ELSE
      CREATE ROLE taskify_auth WITH
	  LOGIN
	  NOSUPERUSER
	  INHERIT
	  CREATEDB
	  CREATEROLE
	  NOREPLICATION
	  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:T867/0S2xBW4Sm4cwD/qJg==$nPYNuG/h+8puWy/h3/t0QmIlijsivCX2b2Mrr8+U1Y8=:bGPOGYGv+GCF+Z3ZRZCWNk4TqeKUwQP5aK9Ks0o/+fA=';
   END IF;
END
$do$;
