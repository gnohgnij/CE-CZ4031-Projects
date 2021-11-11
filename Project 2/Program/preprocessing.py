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
        
        parse_query_plan = (json.dumps(self.query_plan, sort_keys=False, indent=4))
        return parse_query_plan

if __name__ == '__main__':
    query = "SELECT sum(l_extendedprice * l_discount) as revenue FROM lineitem WHERE l_shipdate >= date '1994-01-01' AND l_shipdate < date '1994-01-01' + interval '1' year AND l_discount between 0.06 - 0.01 AND 0.06 + 0.01 AND l_quantity < 24"
    connect = ConnectAndQuery('localhost', '5432', 'test', 'postgres', 'password')
    sample = connect.getQueryPlan(query)

    



