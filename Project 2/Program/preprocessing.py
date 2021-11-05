"""
contains code for reading inputs and any preprocessing necessary to make your algorithm work
"""
import psycopg2
import json
from annotation import *

class ConnectAndQuery():

    def __init__(self, host, port, database, username, password):

        self.connect = psycopg2.connect(host=host, port=port, database=database, user=username, password=password)
        self.cur = self.connect.cursor()

    def getQueryPlan(self, query=None):
        
        self.query_plan = ""

        if query:
            self.query = query
            try:
                self.cur.execute("EXPLAIN (ANALYZE, FORMAT JSON)" + self.query)
                plan = self.cur.fetchall()
                self.query_plan = plan[0][0][0]["Plan"]

            except Exception as e:
                print ("\nError: %s" %str(e))
                self.connect.rollback()

        else:
            self.query_plan = "Failed to generate query plan!"

        #easier to see, can take out later
        with open('query_plan.json', 'w', encoding='utf-8') as f:
            json.dump(self.query_plan, f, ensure_ascii=False, indent=4)
        
        parse_query_plan = (json.dumps(self.query_plan, sort_keys=False, indent=4))
        # return json.dumps(self.query_plan, sort_keys=False, indent=4)
        return parse_query_plan

if __name__ == '__main__':

    query = "SELECT sum(l_extendedprice * l_discount) as revenu FROM lineitem WHERE l_shipdate >= date '1994-01-01' AND l_shipdate < date '1994-01-01' + interval '1' year AND l_discount between 0.06 - 0.01 AND 0.06 + 0.01 AND l_quantity < 24;"
    #1 query = "SELECT l_returnflag,l_linestatus,sum(l_quantity) as sum_qty,sum(l_extendedprice) as sum_base_price,sum(l_extendedprice * (1 - l_discount)) as sum_disc_price,sum(l_extendedprice * (1 - l_discount) * (1 + l_tax)) as sum_charge,avg(l_quantity) as avg_qty,avg(l_extendedprice) as avg_price,avg(l_discount) as avg_disc,count(*) as count_order FROM lineitem WHERE l_shipdate <= date '1998-12-01' - interval '90' day GROUP BY l_returnflag, l_linestatus ORDER BY l_returnflag, l_linestatus;"
    #3 query = "SELECT l_orderkey, sum(l_extendedprice * (1 - l_discount)) as revenue, o_orderdate, o_shippriority FROM customer, orders, lineitem WHERE c_mktsegment = 'BUILDING' AND c_custkey = o_custkey AND l_orderkey = o_orderkey AND o_orderdate < date '1995-03-15' AND l_shipdate > date '1995-03-15' GROUP BY l_orderkey, o_orderdate, o_shippriority ORDER BY revenue desc, o_orderdate LIMIT 20;"
    #5 query = "SELECT n_name, sum(l_extendedprice * (1 - l_discount)) as revenue FROM customer, orders, lineitem, supplier, nation, region WHERE c_custkey = o_custkey AND l_orderkey = o_orderkey AND l_suppkey = s_suppkey AND c_nationkey = s_nationkey AND s_nationkey = n_nationkey AND n_regionkey = r_regionkey AND r_name = 'ASIA' AND o_orderdate >= date '1994-01-01' AND o_orderdate < date '1994-01-01' + interval '1' year GROUP BY n_name ORDER BY revenue desc;"
    #6 query = "SELECT sum(l_extendedprice * l_discount) as revenue FROM lineitem WHERE l_shipdate >= date '1994-01-01' AND l_shipdate < date '1994-01-01' + interval '1' year AND l_discount between 0.06 - 0.01 AND 0.06 + 0.01 AND l_quantity < 24;"
    #10 query = "SELECT c_custkey, c_name, sum(l_extendedprice * (1 - l_discount)) as revenue, c_acctbal, n_name, c_address, c_phone, c_comment FROM customer, orders, lineitem, nation WHERE c_custkey = o_custkey AND l_orderkey = o_orderkey AND o_orderdate >= date '1993-10-01' AND o_orderdate < date '1993-10-01' + interval '3' month AND l_returnflag = 'R' AND c_nationkey = n_nationkey GROUP BY c_custkey, c_name, c_acctbal, c_phone, n_name, c_address, c_comment ORDER BY revenue desc LIMIT 20;"
    #12 query = "SELECT l_shipmode, sum(case when o_orderpriority = '1-URGENT' OR o_orderpriority = '2-HIGH' then 1 else 0 end) as high_line_count, sum(case when o_orderpriority <> '1-URGENT' AND o_orderpriority <> '2-HIGH' then 1 else 0 end) AS low_line_count FROM orders, lineitem WHERE o_orderkey = l_orderkey AND l_shipmode in ('MAIL', 'SHIP') AND l_commitdate < l_receiptdate AND l_shipdate < l_commitdate AND l_receiptdate >= date '1994-01-01' AND l_receiptdate < date '1994-01-01' + interval '1' year GROUP BY l_shipmode ORDER BY l_shipmode;"
    #14 query = "SELECT 100.00 * sum(case when p_type like 'PROMO%' then l_extendedprice * (1 - l_discount) else 0 end) / sum(l_extendedprice * (1 - l_discount)) as promo_revenue FROM lineitem, part WHERE l_partkey = p_partkey AND l_shipdate >= date '1995-09-01' AND l_shipdate < date '1995-09-01' + interval '1' month;"
    #19 query = "SELECT sum(l_extendedprice* (1 - l_discount)) as revenue FROM lineitem, part WHERE ( p_partkey = l_partkey AND p_brand = 'Brand#12' AND p_container in ('SM CASE', 'SM BOX', 'SM PACK', 'SM PKG') AND l_quantity >= 1 AND l_quantity <= 1 + 10 AND p_size between 1 AND 5 AND l_shipmode in ('AIR', 'AIR REG') AND l_shipinstruct = 'DELIVER IN PERSON' ) OR ( p_partkey = l_partkey AND p_brand = 'Brand#23' AND p_container in ('MED BAG', 'MED BOX', 'MED PKG', 'MED PACK') AND l_quantity >= 10 AND l_quantity <= 10 + 10 AND p_size between 1 AND 10 AND l_shipmode in ('AIR', 'AIR REG') AND l_shipinstruct = 'DELIVER IN PERSON' ) OR ( p_partkey = l_partkey AND p_brand = 'Brand#34' AND p_container in ('LG CASE', 'LG BOX', 'LG PACK', 'LG PKG') AND l_quantity >= 20 AND l_quantity <= 20 + 10 AND p_size between 1 AND 15 AND l_shipmode in ('AIR', 'AIR REG') AND l_shipinstruct = 'DELIVER IN PERSON' ); "
    connect = ConnectAndQuery('localhost', '5432', 'test', 'postgres', 'admin123')
    sample = connect.getQueryPlan(query)

    



